using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour
{
    protected enum PlayerType { Food, Tower};
    [SerializeField] protected PlayerType myType;
    public bool isFood;
    public bool isTower;
    public int hitPoints;
    public bool hasFocus = false;

    private void Update()
    {
        if(isFood)
        {
            isTower = false;
        }
        else if(isTower)
        {
            isFood = false;
        }
        //isFood ? true : isTower = false;
    }
}
