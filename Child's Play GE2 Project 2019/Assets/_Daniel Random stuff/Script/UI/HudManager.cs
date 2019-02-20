using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    //External References
    [SerializeField] public GameObject infoPanel;
    [SerializeField] public GameObject storePanel;
    [SerializeField] public GameObject hudPanel;
    [SerializeField] private EnemyManager _enemyManagerRef;
    private AlexGMTest _gmRef;
    private PlayerManager _playerManagerRef;

    private InfoPanel infoPanelScript;


    //Hud Properties



    void Start()
    {
        infoPanelScript = infoPanel.gameObject.GetComponent<InfoPanel>();
        _gmRef = GameObject.Find("AlexGMTest").GetComponent<AlexGMTest>();
        _playerManagerRef = PlayerManager.GetInstance();
    }


    void Update()
    {
        
    }

    public void TowerSelect(StoreButton pressed)
    {
        Debug.Log("Button Registering");
        SetPanel(infoPanel);
        //Display(pressed.Name, pressed.Desc, pressed.Cost);
        _gmRef.SetTowerSelectionIndex(pressed.MyIndex);
        _gmRef.StoreButtonPressed();
    }

    public void Display(string name, string desc, int cost)
    {
        infoPanelScript._name.text = name;
        infoPanelScript._description.text = desc;
        infoPanelScript._cost.text = "Cost: " + cost.ToString();
    }

    public void SetPanel(GameObject panel)
    {
        panel.SetActive(true);   
    }

    public void DeActivatePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
