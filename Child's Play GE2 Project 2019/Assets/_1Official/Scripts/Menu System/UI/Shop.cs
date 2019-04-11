using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ButtonType
{
    Buy,
    Sell,
    Upgrade
}

public class Shop : MonoBehaviour
{
    #region Singleton
    private static Shop instance = null;

    public static Shop GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<Shop>();
        }
        return instance;
    }
    #endregion

    //[SerializeField] private Text toolTipText;
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Text priceT;
    [SerializeField] private Text priceB;
    [SerializeField] private Text priceU;
    [SerializeField] private GameObject boundary;
   
    private const int placeholder = 0;
    private const int shopPanel = 1;
    private const int upgradeSellPanel = 2;
    private const int barrierPanel = 3;
    private int currentPanel = 1;
    private bool onButton;
    private Vector3 compareV;
    private Vector3 rootPos;

    public GameObject[] Panels { get => panels; set => panels = value; }
    public int Placeholder { get => placeholder; }
    public int ShopPanel { get => shopPanel;}
    public int UpgradeSellPanel { get => upgradeSellPanel; }
    public int BarrierPanel { get => barrierPanel; }
    //public int CurrentPanel { get => currentPanel; set => currentPanel = value; }
    public bool OnButton { get => onButton; set => onButton = value; }
    public Vector3 CompareV { get => compareV; set => compareV = value; }

    //public bool Move { get => move; set => move = value; }

    public void SetPanelActive(int panelIndex)
    {
        currentPanel = panelIndex;
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(panelIndex == i);
        }
        switch (currentPanel)
        {
            case shopPanel:
                break;
            case upgradeSellPanel:
                break;
            case barrierPanel:
                break;
            default:
                break;
        }
    }

    public void MoveToClick()
    {
        rootPos = Input.mousePosition;
        Panels[currentPanel].transform.position = rootPos;
    }

    public void TowerSelect(int index)
    {
        GameManager.GetInstance().SetTowerSelectionIndex(index);
        GameManager.GetInstance().StoreButtonPressed();
    }

    public void ChangePrice(Item item, ButtonType buttonType)
    {
        if (buttonType == ButtonType.Buy)
        {
            if (priceT.IsActive())
            {
                priceT.text = $"Price\n{item.Value.ToString()}";
            }
            else priceB.text = $"Price\n{item.Value.ToString()}";
        }
        else if (buttonType == ButtonType.Upgrade)
        {
            Item itemOnTile = GameManager.GetInstance().SelectedItem;
            priceU.text = $"Cost\n{itemOnTile.UpgradeVersion.Value.ToString()}";
        }
        else
        {
            Item itemOnTile = GameManager.GetInstance().SelectedItem;
            priceU.text = $"Value\n{itemOnTile.Value.ToString()}";
        }
    }

    public void TogglePrice(bool show = true)
    {
        priceT.enabled = show;
        priceB.enabled = show;
        priceU.enabled = show;
    }
}
