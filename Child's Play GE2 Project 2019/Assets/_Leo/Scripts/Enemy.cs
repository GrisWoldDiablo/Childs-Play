using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBaseClass
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
        if (this.hasFocus)
        {
            EnemyManager.instance.UpdateEnemyWithFocus();
        }
        
    }

    private void OnMouseOver()
    {        
        if(Input.GetMouseButtonDown(0))
        {
            _cameraController.isLocked = true;

            EnemyManager.instance.ClearEnemyFocus();
            this.hasFocus = true;
            EnemyManager.instance.UpdateEnemyWithFocus();
        }
    }
}
