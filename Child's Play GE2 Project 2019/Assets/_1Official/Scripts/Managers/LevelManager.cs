using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Level
{
    [SerializeField] private string name;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private int initialMoney;
    [Header("Set item available, Including upgrades")]
    [SerializeField] private Item[] itemsAvailable;

    public GameObject LevelPrefab { get => levelPrefab; }
    public int InitialMoney { get => initialMoney; }
    public string Name { get => name; }
    public Item[] ItemsAvailable { get => itemsAvailable; }
}

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager instance = null;

    public static LevelManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<LevelManager>();
        }
        return instance;
    }
    #endregion

    [SerializeField] Level[] levels;
    private GameObject currentLevelGO;
    private int currentLevel = 0;
    private bool currLvlCompleted = false;
    private Transform root;
    private bool levelSpawningCompleted = false;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public bool LevelSpawningCompleted { get => levelSpawningCompleted; }
    public GameObject CurrentLevelGO { get => currentLevelGO; }
    public Level CurrentLevelInfo { get => levels[currentLevel]; }

    //public GameObject CurrLvlObj { get => currLvlObj; set => currLvlObj = value; }

    void Start()
    {
        root = GameObject.FindGameObjectWithTag("Root").transform;
        currentLevel = Settings.GetInstance().StartingLevel;
        LoadLevel(currentLevel);
    }

    /// <summary>
    /// Load the next level in line
    /// </summary>
    public void LoadNextLevel()
    {
        LoadLevel(++currentLevel);
    }

    /// <summary>
    /// Load the level as per index
    /// </summary>
    /// <param name="levelNumber">Level to load</param>
    public void LoadLevel(int levelNumber = 0)
    {
        if (levelNumber >= levels.Length)
        {
            GameCompleted();
            return;
        }
        UpdateSettings();

        ScoreManager.GetInstance().Reset();
        if (currentLevelGO != null)
        {
            DestroyImmediate(currentLevelGO);
        }

        currentLevelGO = Instantiate(levels[levelNumber].LevelPrefab, root.position, root.rotation, null);
        MoneyManager.GetInstance().ResetMoney(levels[levelNumber].InitialMoney);
        EnemyManager.GetInstance().DestroyAllEnemies();
        levelSpawningCompleted = false;

        GameManager.GetInstance().FastForwardButton.Init();
        GameManager.GetInstance().DeselectTile();

        PlayerManager.GetInstance().CreatePlayerList();
        CameraManager.GetInstance().CameraLockerButton();
    }


    /// <summary>
    /// Set the Level to completed mode, reference used by enemy manager 
    /// to call score compile when last enemy dies after.
    /// </summary>
    public void LevelCompleted()
    {
        levelSpawningCompleted = true;
        
    }

    /// <summary>
    /// Call the wining panel, when last level is completed.
    /// </summary>
    public void GameCompleted()
    {
        GameManager.GetInstance().PanelSelection(GameManager.GetInstance().WinPanelIndex);
    }

    public void UpdateSettings()
    {
        if (Settings.GetInstance().LevelsUnlocked < currentLevel)
        {
            Settings.GetInstance().LevelsUnlocked = currentLevel;
            Settings.GetInstance().SaveLevelParams();
        }
    }
}
