using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBaseClass
{
    private CameraController _cameraController;

    // Start is called before the first frame update
    void Start()
    {
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            _cameraController.isLocked = true;

            PlayerManager.instance.ClearEnemyFocusOnListAndCamera();
            this.hasFocus = true;
            PlayerManager.instance.playerWithFocus = this;
            
        }
    }
}
