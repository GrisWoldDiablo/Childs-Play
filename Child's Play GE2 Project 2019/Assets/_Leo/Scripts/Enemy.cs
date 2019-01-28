using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBaseClass
{

    // Start is called before the first frame update
    void Start()
    {
        
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
        //Debug.Log("mousing over");
        if(Input.GetMouseButtonDown(0))
        {
            EnemyManager.instance.ClearEnemyFocus();
            this.hasFocus = true;
            EnemyManager.instance.UpdateEnemyWithFocus();
        }
    }
}
