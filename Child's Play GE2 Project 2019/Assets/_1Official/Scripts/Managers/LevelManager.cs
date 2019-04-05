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

    public GameObject LevelPrefab { get => levelPrefab; }
    public int InitialMoney { get => initialMoney; }
    public string Name { get => name; set => name = value; }
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
    private bool levelSpawningCompleted;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public bool LevelSpawningCompleted { get => levelSpawningCompleted; }
    public GameObject CurrentLevelGO { get => currentLevelGO; }

    //public GameObject CurrLvlObj { get => currLvlObj; set => currLvlObj = value; }

    void Start()
    {
        root = GameObject.FindGameObjectWithTag("Root").transform;
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //if (finishSpawn && EnemyManager.GetInstance().ListOfEnemies.Count <= 0)
        //{
        //    currentLvl++;
        //    LoadLvl();
        //    finishSpawn = false;
        //}
    }

    public void LoadNextLevel()
    {
        LoadLevel(++currentLevel);
    }

    public void LoadLevel(int levelNumber = 0)
    {
        if (levelNumber >= levels.Length)
        {
            GameCompleted();
            return;
        }

        ScoreManager.GetInstance().Reset();
        if (currentLevelGO != null)
        {
            Destroy(currentLevelGO);
        }

        currentLevelGO = Instantiate(levels[levelNumber].LevelPrefab, root.position, root.rotation, null);
        MoneyManager.GetInstance().ResetMoney(levels[levelNumber].InitialMoney);
        EnemyManager.GetInstance().DestroyAllEnemies();
        //if (currentLvl >= levels.Length)
        //{
        //    //GameManager.gameCompleted = true;
        //    Debug.Log("YOU WON");
        //    Destroy(currentLevelGO);
        //}
        //else
        //{
        //    if (currentLevelGO)
        //    {
        //        Destroy(currentLevelGO);
        //    }
        //    currentLevelGO = Instantiate(levels[currentLvl], root);
        //}
    }


    public void LevelCompleted()
    {
        levelSpawningCompleted = true;
    }

    public void GameCompleted()
    {
        GameManager.GetInstance().PanelSelection(GameManager.GetInstance().WinPanelIndex);
    }
}
