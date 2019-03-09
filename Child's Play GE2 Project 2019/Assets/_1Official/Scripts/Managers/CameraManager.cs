using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("DEBUG")]
    public bool disableMouseMovement = false;
#endif

    #region Singleton
    private static CameraManager instance = null;

    public static CameraManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<CameraManager>();
        }
        return instance;
    }
    #endregion

    //Zoom Variables
    public float zoomSpeed = 4f;
    public float maxZoom = 10f;
    public float minZoom = 3f;
    public float pitch;
    [SerializeField]
    private float _currentZoom;

    //Rotation Variables
    public float yawSpeed = 100f;
    public Vector3 cameraArm;
    [SerializeField]
    private float _currentYaw;
    
    //Camera Control Variables
    [SerializeField]
    public bool isLocked = true;
    private Vector3 _cameraFreeMovement;
    [SerializeField]
    private float _cameraTranslationSpeed = 5f;

    //Camera Height Adjustments
    public LayerMask inferiorLayerMask = -1;
    public float lerpHeight;

    //Screen Limits
    private float _screenBorder = 20f;

    //Camera Target
    public Transform actorWithFocus;
    public Transform enemyWithFocus;

    private Transform _playerWithFocus;
    

    //Getters
    public Vector2 GetKeyboardInput
    {
        get { return new Vector2(Input.GetAxis("LateralCameraMovement"), Input.GetAxis("ForwardBackCameraMovement")); }
    }

    public Vector2 GetMousePosition
    {
        get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
    }

    #region UNITY methods
    private void LateUpdate()
    {
        CameraFollowPlayer();
        CameraZoomAndRotationFreeMode();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        YawCorrection();
        CameraZoomAndRotationWhenLocked();
        CameraMovementFreeModeWithKeyboard();
#if UNITY_EDITOR
        if (!disableMouseMovement)
        {
#endif
            CameraMovementFreeModeWithMouse();
#if UNITY_EDITOR
        }
#endif
                if (Input.GetButtonDown("CameraLockMode"))
        {
            CameraLockerButton();
        }
    }
    #endregion

    #region Class methods

    private void CameraFollowPlayer()
    {
        if (PlayerManager.GetInstance().playerWithFocus == null)
        {
            return;
        }
        _playerWithFocus = PlayerManager.GetInstance().playerWithFocus.transform;
        if (EnemyManager.GetInstance().enemyWithFocus != null)
        {
            enemyWithFocus = EnemyManager.GetInstance().enemyWithFocus.transform;
        }

        if (!isLocked)
        {
            return;
        }
        //if (enemyWithFocus != null)
        //{            
        //    actorWithFocus = enemyWithFocus;
        //}
        //else
        //{
        actorWithFocus = _playerWithFocus;
        //}

        this.transform.position = actorWithFocus.position - (cameraArm * _currentZoom);
        this.transform.LookAt(actorWithFocus.position + (Vector3.up * pitch));
        this.transform.RotateAround(actorWithFocus.position, Vector3.up, _currentYaw);
    }

    private void CameraZoomAndRotationWhenLocked()
    {
        if (this.isLocked)
        {
            _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);

            _currentYaw += Input.GetAxis("RotateCamera") * yawSpeed * Time.fixedDeltaTime;           
        }
    }

    private void CameraMovementFreeModeWithKeyboard()
    {
        if (Input.GetButton("ForwardBackCameraMovement") || Input.GetButton("LateralCameraMovement"))
        {
            this.isLocked = false;
            _cameraFreeMovement = new Vector3(GetKeyboardInput.x, 0, GetKeyboardInput.y);
            _cameraFreeMovement *= _cameraTranslationSpeed * Time.fixedDeltaTime;
            _cameraFreeMovement = Quaternion.Euler(new Vector3(0, this.transform.eulerAngles.y, 0)) * _cameraFreeMovement;
            _cameraFreeMovement = this.transform.InverseTransformDirection(_cameraFreeMovement);

            this.transform.Translate(_cameraFreeMovement);
        }
    }

    private void CameraMovementFreeModeWithMouse()
    {
        if (Input.mousePosition.y >= Screen.height - _screenBorder || Input.mousePosition.y <= _screenBorder || Input.mousePosition.x >= Screen.width - _screenBorder || Input.mousePosition.x <= _screenBorder)
        {
            this.isLocked = false;

            Rect leftRect = new Rect(0, 0, _screenBorder, Screen.height);
            Rect rightRect = new Rect(Screen.width - _screenBorder, 0, _screenBorder, Screen.height);
            Rect upperRect = new Rect(0, Screen.height - _screenBorder, Screen.width, _screenBorder);
            Rect lowerRect = new Rect(0, 0, Screen.width, _screenBorder);

            _cameraFreeMovement.x = leftRect.Contains(Input.mousePosition) ? -1 : rightRect.Contains(Input.mousePosition) ? 1 : 0;
            _cameraFreeMovement.z = upperRect.Contains(Input.mousePosition) ? 1 : lowerRect.Contains(Input.mousePosition) ? -1 : 0;

            _cameraFreeMovement *= _cameraTranslationSpeed * Time.fixedDeltaTime;
            _cameraFreeMovement = Quaternion.Euler(new Vector3(0f, this.transform.eulerAngles.y, 0f)) * _cameraFreeMovement;
            _cameraFreeMovement = this.transform.InverseTransformDirection(_cameraFreeMovement);

            this.transform.Translate(_cameraFreeMovement);
        }
    }

    private void CameraZoomAndRotationFreeMode()
    {
        if (!this.isLocked)
        {
            if (Input.GetButton("RotateCamera"))
            {
                this.transform.Rotate(Vector3.up, Input.GetAxis("RotateCamera") * 2 * yawSpeed * Time.fixedDeltaTime * .3f, Space.World);
                _currentYaw += Input.GetAxis("RotateCamera") * yawSpeed * Time.fixedDeltaTime;                
            }

            _currentZoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);

            lerpHeight = _currentZoom;
            float difference = 0f;

            if (DistanceToTheGround() != lerpHeight)
            {
                difference = lerpHeight - DistanceToTheGround();
            }

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, lerpHeight + difference, this.transform.position.z), Time.deltaTime * 5f);
        }
    }    

    private void YawCorrection()
    {
        if(_currentYaw > 360)
        {
            _currentYaw = 0;
        }
        else if(_currentYaw <= 0)
        {
            _currentYaw = 360;
        }
    }

    public float DistanceToTheGround()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, inferiorLayerMask.value))
        {
            return (hit.point - this.transform.position).magnitude;
        }

        return 0f;
    }

    public void CameraLockerButton()
    {        
        this.isLocked = !this.isLocked;
    }

    #endregion




}
