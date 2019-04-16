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
            if (GameManager.GetInstance() != null)
            {
                instance = GameManager.GetInstance().gameObject.AddComponent<EnemyManager>(); 
            }
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
    //void Start()
    //{
    //    //cameraLocker = Camera.main.GetComponent<CameraController>();
    //    UpdateEnemyList();
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    //ChangePlayerFocusWithButton();
    //    //Debug.Log("NUmber of enemies: " + listOfEnemies.Count);
    //}
    #endregion

    /// <summary>
    /// Populates the list of Enemies
    /// </summary>
    //public void UpdateEnemyList()
    //{
    //    //_enemyArray = GameObject.FindObjectsOfType<Enemy>();
    //    //foreach (Enemy e in _enemyArray)
    //    //{
    //    //    listOfEnemies.Add(e);
    //    //    if (e.HasFocus)
    //    //    {
    //    //        enemyWithFocus = e;
    //    //    }
    //    //}
    //}

    //#region Class Methods
    /// <summary>
    /// Takes out focus on ALL enemies
    /// </summary>
    //public void ClearEnemyFocus()
    //{        
    //    enemyWithFocus = null;
    //    foreach (Enemy e in listOfEnemies)
    //    {
    //        //listOfEnemies.Add(e);
    //        if (e.HasFocus)
    //        {
    //            //enemyWithFocus = e;
    //            e.HasFocus = false;
    //        }
    //    }
    //}
    //public void ClearEnemyFocus()
    //{
    //    enemyWithFocus = null;
    //}
    ///// <summary>
    ///// Updates "enemyWithFocus" reference
    ///// </summary>
    //public void UpdateEnemyWithFocus()
    //{
    //    enemyWithFocus = null;
    //    foreach (Enemy e in listOfEnemies)
    //    {
    //        if (e.HasFocus)
    //        {
    //            enemyWithFocus = e;                
    //        }
    //    }
    //}
    //#endregion

    private void Update()
    {
        ChangeEnemyFocusWithButton();
    }

    public void ChangeEnemyFocusWithButton()
    {
        if (Input.GetButtonDown("SwitchEnemy"))
        {
            CameraManager.GetInstance().EnemyWithFocus = NextEnemyInList(CameraManager.GetInstance().EnemyWithFocus);
            CameraManager.GetInstance().isLocked = true;
        }
    }

    public Enemy NextEnemyInList(Enemy enemy)
    {
        if (enemy == null)
        {
            if (listOfEnemies.Count > 0)
            {
                return listOfEnemies[0];
            }
        }
        else
        {
            int indexOfEnemy = listOfEnemies.IndexOf(enemy);
            if (indexOfEnemy < 0)
            {
                return NextEnemyInList(null); ;
            }
            else
            {
                indexOfEnemy++;
                if (indexOfEnemy < listOfEnemies.Count)
                {
                    return listOfEnemies[indexOfEnemy];
                }
            }
        }
        return null;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        listOfEnemies.Add(enemy);
        ScoreManager.GetInstance().EnemyCounts++;
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        listOfEnemies.Remove(enemy);
        if (LevelManager.GetInstance().LevelSpawningCompleted && listOfEnemies.Count == 0)
        {
            ScoreManager.GetInstance().CompileScore();
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (var item in GameObject.FindObjectsOfType<Enemy>())
        {
            Destroy(item.gameObject);
        }
    }
}
