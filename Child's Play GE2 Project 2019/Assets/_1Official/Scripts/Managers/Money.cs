using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int currentMoney;
    public int CurrentMoney { get => currentMoney; set => currentMoney = value; }


    public void Start()
    {
        UpdateMoneyText();
    }

    public void ResetMoney()
    {
        currentMoney = 0;
    }

    public void MoneyChange(int change)
    {
        CurrentMoney += change;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        HudManager.GetInstance().MoneyTxt.text = $"{currentMoney}";
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