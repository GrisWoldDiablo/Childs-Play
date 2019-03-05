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
    //[SerializeField] private EnemyManager _enemyManagerRef;
    [SerializeField] private MenuInteraction _menuInteractionRef;
    [SerializeField] private AlexGMTest _gmRef;

    private PlayerManager _playerManagerRef;
    private HudComponent _hudComponentRef;
    private InfoPanel infoPanelScript;

    private int gameOverIndex;

    //Hud Properties

    void Start()
    {
        Pause.GetInstance().UnPauseGame();
        infoPanelScript = infoPanel.gameObject.GetComponent<InfoPanel>();
        _hudComponentRef = hudPanel.GetComponent<HudComponent>();
        _gmRef = GameObject.Find("AlexGMTest").GetComponent<AlexGMTest>();
        _playerManagerRef = PlayerManager.GetInstance();
        gameOverIndex = 7;
    }

    void Update()
    {
        HudUpdate();
        GameOver();
    }

    public void HudUpdate()
    {
            _hudComponentRef.MoneyTxt.text = $"{_gmRef.MyMoney.CurrentMoney}";
            _hudComponentRef.FoodPercentageTxt.text = $"{_hudComponentRef.FoodRemaining} %";      
    }

    public void TowerSelect(StoreButton pressed)
    {
        _gmRef.SetTowerSelectionIndex(pressed.MyIndex);
        _gmRef.StoreButtonPressed();
    }

    public void Display(GameObject obj)
    {
        var _obj = obj.GetComponent<Item>();
        infoPanelScript._name.text = _obj.ItemName;
        infoPanelScript._description.text = _obj.ItemDescription;
        infoPanelScript._cost.text = _obj.Value.ToString();
    }

    public void GameOver()
    {
        if (AlexGMTest.gameOver)
        {
            _menuInteractionRef.PanelToggle(gameOverIndex);
        }
    }
}
