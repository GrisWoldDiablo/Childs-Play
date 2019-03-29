using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] GameObject[] levels;
    private GameObject currLvlObj;
    private int currentLvl = 0;
    private bool currLvlCompleted = false;
    private Transform root;
    private bool finishSpawn;

    public int CurrentLvl { get => currentLvl; set => currentLvl = value; }
    public GameObject CurrLvlObj { get => currLvlObj; set => currLvlObj = value; }

    void Start()
    {
        root = GameObject.FindGameObjectWithTag("Root").transform;
        LoadLvl();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishSpawn && EnemyManager.GetInstance().ListOfEnemies.Count <= 0)
        {
            currentLvl++;
            LoadLvl();
            finishSpawn = false;
        }
    }

    public void LoadLvl()
    {
        if (currentLvl >= levels.Length)
        {
            GameManager.gameCompleted = true;
            Debug.Log("YOU WON");
            Destroy(currLvlObj);
        }
        else
        {
            if (currLvlObj)
            {
                Destroy(currLvlObj);
            }
            currLvlObj = Instantiate(levels[currentLvl], root);
        }
    }

    public void FinishedSpawn()
    {
        finishSpawn = true;
    }
}
