using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int currentMoney;
    //public int CurrentMoney { get => currentMoney; set => currentMoney = value; }


    public void Start()
    {
        UpdateMoneyText();
    }

    public void ResetMoney(int setMoney = 0)
    {
        currentMoney = setMoney;
    }

    public void MoneyChange(int change)
    {
        if (change > 0)
        {

        }
        else
        {
            ScoreManager.GetInstance().MoneySpent += change;
        }
        currentMoney += change;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        HudManager.GetInstance().UpdateMoneyAmount(currentMoney);
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