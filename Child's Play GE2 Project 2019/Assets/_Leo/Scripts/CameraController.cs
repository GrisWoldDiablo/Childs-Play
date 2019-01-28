using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
        CameraZoomAndRotationFreeModeWithKeyBoard();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        CameraZoomAndRotationWhenLocked();
        CameraZoomAndRotationFreeModeWithMouse();
    }
    #endregion

    #region Class methods

    private void CameraFollowPlayer()
    {
        _playerWithFocus = PlayerManager.instance.playerWithFocus.transform;
        if (EnemyManager.instance.enemyWithFocus != null)
        {
            enemyWithFocus = EnemyManager.instance.enemyWithFocus.transform;
        }

        if (!isLocked)
        {
            return;
        }
        if (enemyWithFocus != null)
        {
            Debug.Log("if");
            actorWithFocus = enemyWithFocus;
        }
        else// (_playerWithFocus != null)
        {
            Debug.Log("else");
            actorWithFocus = _playerWithFocus;
        }

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

            _currentYaw += Input.GetAxis("RotateCamera") * yawSpeed * Time.deltaTime;

        }
    }

    private void CameraZoomAndRotationFreeModeWithKeyBoard()
    {
        if (Input.GetButton("ForwardBackCameraMovement") || Input.GetButton("LateralCameraMovement"))
        {
            this.isLocked = false;
            _cameraFreeMovement = new Vector3(GetKeyboardInput.x, 0, GetKeyboardInput.y);
            _cameraFreeMovement *= _cameraTranslationSpeed * Time.deltaTime;
            _cameraFreeMovement = Quaternion.Euler(new Vector3(0, this.transform.eulerAngles.y, 0)) * _cameraFreeMovement;
            _cameraFreeMovement = this.transform.InverseTransformDirection(_cameraFreeMovement);

            this.transform.Translate(_cameraFreeMovement);
        }
    }

    private void CameraZoomAndRotationFreeModeWithMouse()
    {
        if(Input.mousePosition.y >= Screen.height - _screenBorder || Input.mousePosition.y <= _screenBorder || Input.mousePosition.x >= Screen.width - _screenBorder || Input.mousePosition.x <= _screenBorder)
        {
            this.isLocked = false;

            Rect leftRect = new Rect(0, 0, _screenBorder, Screen.height);
            Rect rightRect = new Rect(Screen.width - _screenBorder, 0, _screenBorder, Screen.height);
            Rect upperRect = new Rect(0, Screen.height - _screenBorder, Screen.width, _screenBorder);
            Rect lowerRect = new Rect(0, 0, Screen.width, _screenBorder);

            _cameraFreeMovement.x = leftRect.Contains(Input.mousePosition) ? -1 : rightRect.Contains(Input.mousePosition) ? 1 : 0;
            _cameraFreeMovement.z = upperRect.Contains(Input.mousePosition) ? 1 : lowerRect.Contains(Input.mousePosition) ? -1 : 0;

            _cameraFreeMovement *= _cameraTranslationSpeed * Time.deltaTime;
            _cameraFreeMovement = Quaternion.Euler(new Vector3(0f, this.transform.eulerAngles.y, 0f)) * _cameraFreeMovement;
            _cameraFreeMovement = this.transform.InverseTransformDirection(_cameraFreeMovement);

            this.transform.Translate(_cameraFreeMovement);

        }
    }

    #endregion




}
