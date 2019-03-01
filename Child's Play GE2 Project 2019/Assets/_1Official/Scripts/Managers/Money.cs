using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int currentMoney;
    public int CurrentMoney { get => currentMoney; set => currentMoney = value; }

    
    public void ResetMoney()
    {
        currentMoney = 0;
    }

    public void MoneyChange(int change)
    {
        CurrentMoney += change;
        BroadcastMessage("UpdateCashText");
    }

    public bool TryToBuy(int cost)
    {
        if (!CanBuy(cost))
        {
            Debug.Log("Not Enough Cash.");
            return false;
        }
        MoneyChange(-cost);
        return true;
    }

    public bool CanBuy(int cost)
    {
        if (currentMoney >= cost)
        {
            return true;
        }
        return false;
    }
}