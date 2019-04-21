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
    [SerializeField] private List<Enemy> _listOfEnemies = new List<Enemy>();

    //References for Cashing
    private Enemy _enemyWithFocus;
    public List<Enemy> ListOfEnemies { get => _listOfEnemies;}
    public Enemy EnemyWithFocus { get => _enemyWithFocus; set => _enemyWithFocus = value; }

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
            if (_listOfEnemies.Count > 0)
            {
                return _listOfEnemies[0];
            }
        }
        else
        {
            int indexOfEnemy = _listOfEnemies.IndexOf(enemy);
            if (indexOfEnemy < 0)
            {
                return NextEnemyInList(null); ;
            }
            else
            {
                indexOfEnemy++;
                if (indexOfEnemy < _listOfEnemies.Count)
                {
                    return _listOfEnemies[indexOfEnemy];
                }
            }
        }
        return null;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        _listOfEnemies.Add(enemy);
        ScoreManager.GetInstance().EnemyCounts++;
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        _listOfEnemies.Remove(enemy);
        if (LevelManager.GetInstance().LevelSpawningCompleted && _listOfEnemies.Count == 0)
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
