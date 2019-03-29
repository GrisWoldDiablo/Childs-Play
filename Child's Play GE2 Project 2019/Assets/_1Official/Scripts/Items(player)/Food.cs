using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Food : Player
{
    [SerializeField] private GameObject[] pieces;

    private int initialHP;
    private int currentPercentage = 100;

    public int CurrentPercentage { get => currentPercentage; private set => currentPercentage = value; } 

    void Start()
    {
        initialHP = HitPoints;
        pieces = GameObject.FindGameObjectsWithTag("FoodPieces");
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);

        currentPercentage = 100 - (HitPoints * 100 / initialHP);
        int qtyToRemove = (int)Mathf.Floor(pieces.Length * (currentPercentage / 100.0f));
        for (int i = pieces.Length - 1; i > pieces.Length - qtyToRemove; i--)
        {
            if (i < 0)
            {
                break;
            }
            if (pieces[i] != null)
            {
                Destroy(pieces[i]);
            }
        }

        
        HudManager.GetInstance().UpdateFoodPercentage(currentPercentage);
        ScoreManager.GetInstance().FoodPercentage = currentPercentage;
        ScoreManager.GetInstance().FoodEaten += damageValue;
    }

    protected override void Die()
    {
        GameManager.GetInstance().GameOver();
        base.Die();
    }
    
}
