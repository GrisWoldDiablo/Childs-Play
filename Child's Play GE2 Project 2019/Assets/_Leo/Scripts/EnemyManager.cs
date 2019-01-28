using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;        
    }
    #endregion

    //Variables
    private Enemy[] _enemyArray;
    public List<Enemy> listOfEnemies = new List<Enemy>();

    //References for Cashing
    public Enemy enemyWithFocus;
    public CameraController cameraLocker;

    #region Unity API Methods
    // Start is called before the first frame update
    void Start()
    {
        cameraLocker = Camera.main.GetComponent<CameraController>();
        UpdateEnemyList();
    }

    // Update is called once per frame
    void Update()
    {
        //ChangePlayerFocusWithButton();
    }
    #endregion

    #region Class Methods
    /// <summary>
    /// Populates the list of Player's Objects (Towers and Food)
    /// </summary>
    public void UpdateEnemyList()
    {
        _enemyArray = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy e in _enemyArray)
        {
            listOfEnemies.Add(e);
            if (e.hasFocus)
            {
                enemyWithFocus = e;
            }
        }
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
            if (e.hasFocus)
            {
                //enemyWithFocus = e;
                e.hasFocus = false;
            }
        }
    }

    /// <summary>
    /// Updates "enemyWithFocus" reference
    /// </summary>
    public void UpdateEnemyWithFocus()
    {
        //_enemyArray = GameObject.FindObjectsOfType<Enemy>();

        enemyWithFocus = null;
        foreach (Enemy e in listOfEnemies)
        {
            //listOfEnemies.Add(e);
            if (e.hasFocus)
            {
                enemyWithFocus = e;                
            }
        }
    }

    #endregion
}
