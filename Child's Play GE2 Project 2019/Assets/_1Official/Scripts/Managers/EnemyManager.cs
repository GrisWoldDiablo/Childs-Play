using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    private static EnemyManager instance = null;

    public static EnemyManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameManager.GetInstance().gameObject.AddComponent<EnemyManager>();
        }
        return instance;
    }
    #endregion

    //Variables
    //private Enemy[] _enemyArray;
    [SerializeField] private List<Enemy> listOfEnemies = new List<Enemy>();

    //References for Cashing
    public Enemy enemyWithFocus;
    //public CameraController cameraLocker;

    public List<Enemy> ListOfEnemies { get => listOfEnemies;}

    #region Unity API Methods
    // Start is called before the first frame update
    void Start()
    {
        //cameraLocker = Camera.main.GetComponent<CameraController>();
        UpdateEnemyList();
    }

    // Update is called once per frame
    void Update()
    {
        //ChangePlayerFocusWithButton();
        Debug.Log("NUmber of enemies: " + listOfEnemies.Count);
    }
    #endregion

    #region Class Methods
    /// <summary>
    /// Populates the list of Enemies
    /// </summary>
    public void UpdateEnemyList()
    {
        //_enemyArray = GameObject.FindObjectsOfType<Enemy>();
        //foreach (Enemy e in _enemyArray)
        //{
        //    listOfEnemies.Add(e);
        //    if (e.HasFocus)
        //    {
        //        enemyWithFocus = e;
        //    }
        //}
    }

    /// <summary>
    /// Takes out focus on ALL enemies
    /// </summary>
    public void ClearEnemyFocus()
    {        
        enemyWithFocus = null;
        foreach (Enemy e in listOfEnemies)
        {
            //listOfEnemies.Add(e);
            if (e.HasFocus)
            {
                //enemyWithFocus = e;
                e.HasFocus = false;
            }
        }
    }

    /// <summary>
    /// Updates "enemyWithFocus" reference
    /// </summary>
    public void UpdateEnemyWithFocus()
    {
        enemyWithFocus = null;
        foreach (Enemy e in listOfEnemies)
        {
            if (e.HasFocus)
            {
                enemyWithFocus = e;                
            }
        }
    }
    #endregion

    public void AddEnemyToList(Enemy enemy)
    {
        listOfEnemies.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        listOfEnemies.Remove(enemy);
    }


}
