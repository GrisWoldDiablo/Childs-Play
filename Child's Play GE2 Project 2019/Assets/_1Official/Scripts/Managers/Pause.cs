using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    #region Singleton
    public static Pause instance = null;

    public static Pause GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<Pause>();
        }
        return instance;
    }
    #endregion

    private bool paused = false;
    public bool Paused { get => paused;}


    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        paused = false;
        Time.timeScale = 1;
    }

}
