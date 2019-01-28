using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBaseClass
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        //Debug.Log("mousing over");
        if (Input.GetMouseButtonDown(0))
        {
            PlayerManager.instance.ClearEnemyFocus();
            this.hasFocus = true;
            PlayerManager.instance.UpdateEnemyWithFocus();
        }
    }
}
