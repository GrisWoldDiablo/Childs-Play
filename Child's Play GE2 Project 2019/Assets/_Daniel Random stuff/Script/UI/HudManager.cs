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
    [SerializeField] private MenuInteraction _menuInteractionRef;
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
        _gmRef.SetTowerSelectionIndex(pressed.MyIndex);
        _gmRef.StoreButtonPressed();
    }

    public void Display(GameObject obj)
    {
        var _obj = obj.GetComponent<Item>();
        infoPanelScript._name.text = _obj.name;
        infoPanelScript._description.text = _obj.ItemDescription;
        infoPanelScript._cost.text = "Cost: " + _obj.Value.ToString();
    }

    public void Display(string name, string desc, int value)
    {

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
