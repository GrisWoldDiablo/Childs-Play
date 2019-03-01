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
    new protected void Update()
    {
        base.Update();
    }

    private void OnMouseOver()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            clickCounter++;

            _cameraController.isLocked = false;

            PlayerManager.GetInstance().ClearEnemyFocusOnListAndCamera();
            PlayerManager.GetInstance().playerWithFocus = this;  
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
