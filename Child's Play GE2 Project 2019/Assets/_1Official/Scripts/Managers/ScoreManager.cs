using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    private static ScoreManager instance = null;

    public static ScoreManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameManager.FindObjectOfType<ScoreManager>();
        }
        return instance;
    }
    #endregion

    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreDescriptionText;
    // Enemy
    private float enemyCount;
    private float enemyKilled;
    private float enemyEscaped;
    public float EnemyCounts { get => enemyCount; set => enemyCount = value; }
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
        enemyCount = 0;
        enemyKilled = 0;
        enemyEscaped = 0;
    }

    public void CompileScore()
    {
        //Pause.GetInstance().PauseGame();
        scoreDescriptionText.text = $"Money Spent:\n" +
                                    $"Money Earned:\n" +
                                    $"Food Percentage Left:\n" +
                                    $"Food Eaten:\n" +
                                    $"Enemy Count:\n" +
                                    $"Enemy Killed:\n" +
                                    $"Enemy Escaped:";

        scoreText.text = $"{moneySpent}\n" +
                         $"{moneyEarned}\n" +
                         $"{foodPercentage}\n" +
                         $"{foodEaten}\n" +
                         $"{enemyCount}\n" +
                         $"{enemyKilled}\n" +
                         $"{enemyEscaped}";

        GameManager.GetInstance().PanelSelection(GameManager.GetInstance().ScorePanelIndex);
    }
}
