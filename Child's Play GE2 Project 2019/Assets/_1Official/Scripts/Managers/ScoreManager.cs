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
    private int enemyCount;
    private int enemyKilled;
    private int enemyEscaped;
    public int EnemyCounts { get => enemyCount; set => enemyCount = value; }
    public int EnemyKilled { get => enemyKilled; set => enemyKilled = value; }
    public int EnemyEscaped { get => enemyEscaped; set => enemyEscaped = value; }

    // Food
    private float foodPercentage;
    private int foodEaten;
    public float FoodPercentage { get => foodPercentage; set => foodPercentage = value; }
    public int FoodEaten { get => foodEaten; set => foodEaten = value; }

    // Money
    private int moneySpent;
    private int moneyEarned;
    public int MoneySpent { get => moneySpent; set => moneySpent = value; }
    public int MoneyEarned { get => moneyEarned; set => moneyEarned = value; }

    // Score
    private int score;
    public int Score { get => score; }

    public void Reset()
    {
        moneySpent = 0;
        moneyEarned = 0;
        foodPercentage = 100;
        foodEaten = 0;
        enemyCount = 0;
        enemyKilled = 0;
        enemyEscaped = 0;
        score = 0;
    }

    public void CompileScore()
    {
        score -= 10 * moneySpent;
        score += (MoneyManager.GetInstance().CurrentMoney - moneyEarned);
        score += 100 * enemyKilled;
        score -= 10 * foodEaten;
        score -= 10 * enemyEscaped;
        score += 10 * 100 * (int)FoodPercentage;

        scoreDescriptionText.text = $"Money Spent:\n" +
                                    $"Money Earned:\n" +
                                    $"Enemy Killed:\n" +
                                    $"Food Eaten:\n" +
                                    $"Enemy Escaped:\n" +
                                    $"Food Percentage Left:\n" +
                                    $"Enemy Count:\n" +
                                    $"Total Score:";

        scoreText.text = $"(-) {moneySpent}\n" +
                         $"(+) {moneyEarned}\n" +
                         $"(+) {enemyKilled}\n" +
                         $"(-) {foodEaten}\n" +
                         $"(-) {enemyEscaped}\n" +
                         $"(x) {foodPercentage}\n" +
                         $"{enemyCount}\n" +
                         $"{score}\n";
        SoundManager.GetInstance().StopMusic();
        SoundManager.GetInstance().PlaySoundOneShot(Sound.levelCompleted);

        if (Settings.GetInstance().CheckLeaderboard(score,LevelManager.GetInstance().CurrentLevel))
        {
            GameManager.GetInstance().PanelSelection(GameManager.GetInstance().NewRankPanelIndex);
            scoreDescriptionText.text += $"\nNEW HIGHSCORE!";
        }
        else
        {
            GameManager.GetInstance().PanelSelection(GameManager.GetInstance().ScorePanelIndex);
            scoreDescriptionText.text += $"\nHighScore: {Settings.GetInstance().LeaderboardScores[LevelManager.GetInstance().CurrentLevel]} by {Settings.GetInstance().LeaderboardNames[LevelManager.GetInstance().CurrentLevel]} ";
        }

    }
}
