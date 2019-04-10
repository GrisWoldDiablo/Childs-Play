using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    #region Singleton
    private static Pause instance = null;

    public static Pause GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<Pause>();
        }
        return instance;
    }
    #endregion

    /// <summary>
    /// Pause the application, set the TimeScale to 0
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Unpause the application, Set the TimeScale to the value it was before pausing.
    /// </summary>
    public void UnPauseGame()
    {
        Time.timeScale = GameManager.GetInstance().CurrentGameSpeed;
    }

    /// <summary>
    /// Toggle the MainMenu, Pause the game and Unpause accordingly.
    /// </summary>
    public void ToggleMainMenu()
    {
        if (Time.timeScale == GameManager.GetInstance().CurrentGameSpeed)
        {
            PauseGame();
            MenuInteraction.GetInstance().PanelToggle(0);
        }
        else
        {
            UnPauseGame();
            MenuInteraction.GetInstance().PanelToggle();
        }
    }
}
