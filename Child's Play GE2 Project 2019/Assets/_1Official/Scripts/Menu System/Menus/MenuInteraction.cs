///
/// Author: Alexandre Lepage
/// Date: October 2018
/// Desc: Project for LaSalle College
///

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour {

    [Header("Menus")]
    [SerializeField] private GameObject[] panels;
    public GameObject[] Panels { get { return panels; } }
    [SerializeField] private Selectable[] defaultSelections;
    public Selectable[] DefaultSelections { get { return defaultSelections; } }


    //placeholder testing variables
    public int defaultIndex = 5;
    public readonly int storeIndex = 6;


    private int currentPanel;
    List<Selectable> selectables;

    // Use this for initialization
    void Start () {
        selectables = Selectable.allSelectables;
        PanelToggle(defaultIndex);
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

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
