using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
        CreatePlayerList();
    }
    #endregion

    //Variables
    private Player[] _playerArray;
    public List<Player> listOfPlayers = new List<Player>();

    //References for Cashing
    public Player playerWithFocus;
    public CameraController cameraLocker;

    //private CameraController _cameraController;

    #region Unity API Methods
    // Start is called before the first frame update
    void Start()
    {
        cameraLocker = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangePlayerFocusWithButton();
    }
    #endregion

    #region Class Methods
    /// <summary>
    /// Populates the list of Player's Objects (Towers and Food)
    /// </summary>
    private void CreatePlayerList()
    {
        _playerArray = GameObject.FindObjectsOfType<Player>();
        foreach (Player p in _playerArray)
        {
            listOfPlayers.Add(p);
            if(p.isFood)
            {
                //p.hasFocus = true;
                playerWithFocus = p;
            }
        }        
    }

    /// <summary>
    /// Changes the Focus of a Tower or Food by pressing one Button
    /// </summary>
    private void ChangePlayerFocusWithButton()
    {
        if (Input.GetButtonDown("SwitchPlayer"))
        {
            ClearEnemyFocusOnListAndCamera();

            int index = listOfPlayers.IndexOf(playerWithFocus);
            //listOfPlayers[index].hasFocus = false;
            index++;
            if (index >= listOfPlayers.Count)
            {
                index = 0;
            }
            //listOfPlayers[index].hasFocus = true;
            playerWithFocus = listOfPlayers[index];
            cameraLocker.isLocked = true;
        }
    }

    public void ClearEnemyFocusOnListAndCamera()
    {
        EnemyManager.instance.ClearEnemyFocus();
        cameraLocker.enemyWithFocus = null;
    }

    private void ChangePlayerFocusWithMouse()
    {
        EnemyManager.instance.ClearEnemyFocus();
        cameraLocker.enemyWithFocus = null;

        int index = listOfPlayers.IndexOf(playerWithFocus);
        //listOfPlayers[index].hasFocus = false;
        index++;
        if (index >= listOfPlayers.Count)
        {
            index = 0;
        }
        //listOfPlayers[index].hasFocus = true;
        playerWithFocus = listOfPlayers[index];
        cameraLocker.isLocked = true;
    }
    #endregion
}
