﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    #region Singleton
    private static MoneyManager instance = null;

    public static MoneyManager GetInstance()
    {
        if (instance == null)
        {
            if (GameManager.GetInstance() != null)
            {
                instance = GameManager.GetInstance().gameObject.AddComponent<MoneyManager>(); 
            }
        }
        return instance;
    }
    #endregion
    
    private int currentMoney;
    public int CurrentMoney { get => currentMoney; }

    public void ResetMoney(int setMoney = 0)
    {
        currentMoney = setMoney;
        UpdateMoneyText();
    }

    public void MoneyChange(int change)
    {
        if (change < 0)
        {
            ScoreManager.GetInstance().MoneySpent += Mathf.Abs(change);
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
        //if (!CanBuy(cost))
        if (currentMoney >= cost)
        {
            MoneyChange(-cost);
            return true;
        }

        Debug.Log("Not Enough Cash.");
        return false;
    }

    //public bool CanBuy(int cost)
    //{
    //    if (currentMoney >= cost)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}