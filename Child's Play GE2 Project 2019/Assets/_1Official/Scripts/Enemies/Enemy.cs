using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(EnemyController))]
public class Enemy : EnemyBaseClass
{
    //private CameraController _cameraController;

    //NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    //new void Start()
    //{
    //    base.Start();
    //    //_cameraController = Camera.main.GetComponent<CameraController>();
    //}

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (this.hasFocus)
        {
            EnemyManager.GetInstance().UpdateEnemyWithFocus();
        }
    }

    //attackmethid...

    private void OnMouseOver()
    {        
        if(Input.GetMouseButtonDown(0))
        {
            CameraManager.GetInstance().isLocked = true;

            EnemyManager.GetInstance().ClearEnemyFocus();
            this.hasFocus = true;
            EnemyManager.GetInstance().UpdateEnemyWithFocus();
        }
    }
}
