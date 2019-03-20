using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused;

    public bool Paused { get => paused;}

    public static Pause instance = null;

    public static Pause GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<Pause>();
        }
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
