using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    #region Singleton
    private static HudManager instance = null;

    public static HudManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<HudManager>();
        }
        return instance;
    }
    #endregion

    //External References
    [SerializeField] public GameObject infoPanel;
    [SerializeField] public GameObject storePanel;
    [SerializeField] public GameObject hudPanel;
    //[SerializeField] private EnemyManager _enemyManagerRef;
    //[SerializeField] private MenuInteraction _menuInteractionRef;
    //[SerializeField] private GameManager _gmRef;

    //private PlayerManager _playerManagerRef;
    //private HudComponent _hudComponentRef;
    private InfoPanel infoPanelScript;

    //public HudComponent HudComponentRef { get => _hudComponentRef;}
    [Header("HUD Components")]
    [SerializeField] private Image fillerImage;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private Text foodPercentageTxt;
    [SerializeField] private Text warmUpText;
    //private float foodRemaining;

    //public Text MoneyTxt { get => moneyTxt; set => moneyTxt = value; }
    //public Text FoodPercentageTxt { get => foodPercentageTxt; set => foodPercentageTxt = value; }
    //public float FoodRemaining { get => foodRemaining; set => foodRemaining = value; }
    //public Text WarmUpText { get => warmUpText; set => warmUpText = value; }
    //public Image FillerImage { get => fillerImage; set => fillerImage = value; }

    //private int gameOverIndex;

    //Hud Properties

    void Start()
    {
        Pause.GetInstance().UnPauseGame();
        infoPanelScript = infoPanel.gameObject.GetComponent<InfoPanel>();
        //_hudComponentRef = GetComponent<HudComponent>();
        //_gmRef = GameObject.Find("AlexGMTest").GetComponent<GameManager>();
        //_playerManagerRef = PlayerManager.GetInstance();
        //gameOverIndex = 7;
    }

    void Update()
    {
        //HudUpdate();
        //GameOver();
    }

    //public void HudUpdate()
    //{
    //    moneyTxt.text = $"{GameManager.GetInstance().MyMoney.CurrentMoney}";
    //}

    public void TowerSelect(StoreButton pressed)
    {
        GameManager.GetInstance().SetTowerSelectionIndex(pressed.MyIndex);
        GameManager.GetInstance().StoreButtonPressed();
    }

    public void Display(GameObject obj)
    {
        var _obj = obj.GetComponent<Item>();
        infoPanelScript._name.text = _obj.ItemName;
        infoPanelScript._description.text = _obj.ItemDescription;
        infoPanelScript._cost.text = _obj.Value.ToString();
    }

    public void UpdateFoodPercentage(float percentage)
    {
        float fill = 1.0f - percentage / 100.0f;
        if (fill < 0)
        {
            fill = 0;
        }
        foodPercentageTxt.text = $"{fill.ToString("P0")}";
        fillerImage.fillAmount = fill;
    }
    
    public void UpdateMoneyAmount(int moneyAmount)
    {
        moneyTxt.text = $"{moneyAmount}";
    }

    public void UpdateWarmUpText(float time)
    {
        warmUpText.text = $"Ants Incoming \n{time.ToString()}s";
    }
    
    public void ShowWarmUpText(bool active)
    {
        warmUpText.gameObject.SetActive(false);
    }
}
