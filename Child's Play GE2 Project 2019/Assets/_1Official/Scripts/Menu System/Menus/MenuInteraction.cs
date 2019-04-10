///
/// Author: Alexandre Lepage
/// Date: October 2018
/// Desc: Project for LaSalle College
///

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour {

    #region Singleton
    private static MenuInteraction instance = null;

    public static MenuInteraction GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<MenuInteraction>();
        }
        return instance;
    }
    #endregion

    [Header("Menus")]
    [SerializeField] private int sceneDefaultIndex = 0;
    [SerializeField] private GameObject[] panels;
    public GameObject[] Panels { get { return panels; } }
    [SerializeField] private Selectable[] defaultSelections;
    public Selectable[] DefaultSelections { get { return defaultSelections; } }

    private int currentPanel;
    public bool AtDefaultOrRootPanel { get => currentPanel == sceneDefaultIndex || currentPanel == 0; }

    List<Selectable> selectables;

    // Use this for initialization
    void Start () {
        selectables = Selectable.allSelectables;
        PanelToggle(sceneDefaultIndex);
        //GetLeaderboard();
    }
	
	// Update is called once per frame
	void Update () {
        selectables = Selectable.allSelectables;
        bool oneSelected = false;
        foreach (var item in selectables)
        {
            if (item.GetComponent<SelectableInteraction>().Selected)
            {
                oneSelected = true;
                break;
            }
        }
        if (!oneSelected || selectables.Count == 0)
        {
            defaultSelections[currentPanel].Select();
        }
	}
    /// <summary>
    /// Toggle to the scene's default panel.
    /// </summary>
    public void PanelToggle()
    {
        PanelToggle(sceneDefaultIndex);
    }

    /// <summary>
    /// Show or hide canvas panels as per index param.
    /// </summary>
    /// <param name="panelIndex">Panel index</param>
    public void PanelToggle(int panelIndex)
    {
        currentPanel = panelIndex;
        Input.ResetInputAxes();
        
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(panelIndex == i);
            if (panelIndex == i)
            {
                defaultSelections[i].Select();
            }
        }
    }

    /// <summary>
    /// Call the application to quit, (exit the game) 
    /// Or stop playing in editor.
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
