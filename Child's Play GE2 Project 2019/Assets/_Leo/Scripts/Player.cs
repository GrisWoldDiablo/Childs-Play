using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBaseClass
{
    private CameraController _cameraController;
            
    private int clickCounter;

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
            clickCounter++;


            _cameraController.isLocked = false;

            PlayerManager.instance.ClearEnemyFocusOnListAndCamera();
            this.hasFocus = true;
            PlayerManager.instance.playerWithFocus = this;

            WaitForSeconds waitForSeconds = new WaitForSeconds(1);

            //if(Input.GetMouseButtonDown(0))
            //{
            //    _cameraController.isLocked = true;
            //}
            
        }

        if(clickCounter >= 2)
        {
            _cameraController.isLocked = true;
            clickCounter = 0;
        }
    }

    private void OnMouseExit()
    {
        clickCounter = 0;
    }
}
