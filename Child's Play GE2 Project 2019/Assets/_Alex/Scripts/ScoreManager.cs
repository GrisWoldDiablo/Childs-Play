using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    private static ScoreManager instance = null;

    public static ScoreManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameManager.GetInstance().gameObject.AddComponent<ScoreManager>();
        }
        return instance;
    }
    #endregion
    
    // Enemy
    private float enemyCounts;
    private float enemyKilled;
    private float enemyEscaped;
    public float EnemyCounts { get => enemyCounts; set => enemyCounts = value; }
    public float EnemyKilled { get => enemyKilled; set => enemyKilled = value; }
    public float EnemyEscaped { get => enemyEscaped; set => enemyEscaped = value; }

    // Food
    private float foodPercentage;
    private float foodEaten;
    public float FoodPercentage { get => foodPercentage; set => foodPercentage = value; }
    public float FoodEaten { get => foodEaten; set => foodEaten = value; }

    // Money
    private float moneySpent;
    private float moneyEarned;
    public float MoneySpent { get => moneySpent; set => moneySpent = value; }
    public float MoneyEarned { get => moneyEarned; set => moneyEarned = value; }

    public void Reset()
    {
        moneySpent = 0;
        moneyEarned = 0;
        foodPercentage = 100;
        foodEaten = 0;
        enemyCounts = 0;
        enemyKilled = 0;
        enemyEscaped = 0;
    }
}
