using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyBaseClass
{


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
