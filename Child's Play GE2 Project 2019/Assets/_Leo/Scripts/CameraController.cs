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

    private Transform _playerWithFocus;
    private Transform _enemyWithFocus;

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
        //actorWithFocus = PlayerManager.instance.playerWithFocus.transform;
        CameraFollowPlayer();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        CameraControls();
    }
    #endregion

    #region Class methods

    private void CameraFollowPlayer()
    {
        _playerWithFocus = PlayerManager.instance.playerWithFocus.transform;
        if (EnemyManager.instance.enemyWithFocus != null)
        {
            _enemyWithFocus = EnemyManager.instance.enemyWithFocus.transform;
        }

        if(!isLocked)
        {
            return;
        }
        if(_enemyWithFocus != null)
        {
            actorWithFocus = _enemyWithFocus;
        }
        else if (_playerWithFocus != null)
        {
            actorWithFocus = _playerWithFocus;
        }

        this.transform.position = actorWithFocus.position - (cameraArm * _currentZoom);
        this.transform.LookAt(actorWithFocus.position + (Vector3.up * pitch));
        this.transform.RotateAround(actorWithFocus.position, Vector3.up, _currentYaw);

    }

    private void CameraControls()
    {
        if(this.isLocked)
        {
            _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);

            _currentYaw += Input.GetAxis("RotateCamera") * yawSpeed * Time.deltaTime;

        }
    }

    #endregion




}
