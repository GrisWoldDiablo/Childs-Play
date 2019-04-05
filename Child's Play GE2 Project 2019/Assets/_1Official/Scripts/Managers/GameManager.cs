using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is to be place in the GameManager script
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameManager>();
        }
        return instance;
    }
    #endregion

    //[SerializeField] private float currentcash = 5;
    //[SerializeField] private MenuInteraction _menuInteractionRef;
    [SerializeField] private GameObject tileSelectionCursor;
    [SerializeField] private GameObject tileSelectedCursor;
    [SerializeField] private GameObject[] listOfTower;
    [SerializeField] private GameObject[] listOfTowerPlaceHolder;
    [SerializeField] private GameObject[] listOfBarrier;
    [SerializeField] private GameObject[] listOfBarrierPlaceHolder;
    [SerializeField] private int selectedTowerIndex = 0;
    [SerializeField] private int selectedBarrierIndex = 0;

    //Daniel Temporary
    //[SerializeField] private HudManager _hudManagerRef;
    //private int legoTowerIndex = 1;
    //private int soldierTowerIndex = 0;
    //public static bool gameOver;
    //public static bool gameCompleted;

    //[SerializeField] private int initialMoney = 100;
    //private Money myMoney;

    [Header("Panels Indexes")]
    [SerializeField] private int gameOverPanelIndex = 7;
    [SerializeField] private int scorePanelIndex = 8;
    [SerializeField] private int winPanelIndex = 9;
    public int ScorePanelIndex { get => scorePanelIndex; }
    public int WinPanelIndex { get => winPanelIndex; }

    
    private bool showHealthBars = true;

    private ItemTile selectedTile;

    public ItemTile SelectedTile { get => selectedTile; }
    public GameObject TileSelectionCursor { get => tileSelectionCursor; }

    public int SelectedTowerIndex { get => selectedTowerIndex; set => selectedTowerIndex = value; }
    public int SelectedBarrierIndex { get => selectedBarrierIndex; set => selectedBarrierIndex = value; }
    //public Money MyMoney { get => myMoney; private set => myMoney = value; }
    public bool ShowHealthBars { get => showHealthBars; set => showHealthBars = value; }



    // Start is called before the first frame update
    void Start()
    {
        //gameOver = false;
        ItemSelectionReset(); // for testing
        //UpdateSelectedTileText();
        //Debug.Log("Tower index" +SelectedTowerIndex);
        //myMoney = gameObject.AddComponent<Money>();
        //MoneyManager.GetInstance().ResetMoney(initialMoney); // This value changes at the beginning of new level.
        //UpdateCashText(); // to be place in UI management script
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            DeselectTile();
        }
        //Debug.Log("My game over value:" + gameOver);
    }

    /// <summary>
    /// Deselect the current selected tile,
    /// hide all placeholder items
    /// and call the method to update the selected tile text.
    /// </summary>
    public void DeselectTile()
    {
        tileSelectedCursor.SetActive(false);

        HidePlaceHolders();
        ShowRange(false);
        selectedTile = null;
        //UpdateSelectedTileText();
        Shop.GetInstance().SetPanelActive(Shop.GetInstance().Placeholder);
        //Shop.GetInstance().SetActiveToolTip(false);
        //PanelSelection(MenuInteraction.GetInstance().defaultIndex);
    }

    private void ShowRange(bool show = true)
    {
        if (selectedTile != null)
        {
            if (selectedTile.CurrentItem != null)
            {
                GameObject rangeGO = selectedTile.CurrentItem.GetComponent<Item>().RangeGO;
                if (rangeGO != null)
                {
                    rangeGO.SetActive(show);
                }
            }
        }
    }

    void ItemSelectionReset()
    {
        for (int i = 0; i < listOfBarrierPlaceHolder.Length; i++)
        {
            listOfBarrierPlaceHolder[i] = Instantiate(listOfBarrierPlaceHolder[i]);
        }

        for (int i = 0; i < listOfTowerPlaceHolder.Length; i++)
        {
            listOfTowerPlaceHolder[i] = Instantiate(listOfTowerPlaceHolder[i]);
        }

        HidePlaceHolders();
        Shop.GetInstance().SetPanelActive(Shop.GetInstance().Placeholder);
        //Shop.GetInstance().SetActiveToolTip(false);
        //PanelSelection(MenuInteraction.GetInstance().defaultIndex); //Daniel temporary testing
        tileSelectionCursor.SetActive(false);
        tileSelectedCursor.SetActive(false);
    }

    /// <summary>
    /// Select a tile and sets it as the tile selected.
    /// </summary>
    /// <param name="tile">Tile to select</param>
    public void TileSelection(ItemTile tile)
    {
        HidePlaceHolders();
        ShowCursorOnTile(tileSelectedCursor, tile);
        ShowRange(false);
        selectedTile = tile;
        //UpdateSelectedTileText();
        if (tile.CurrentItem != null)
        {
            //PanelSelection(MenuInteraction.GetInstance().storeIndex);  //Daniel temporary testing
            //_hudManagerRef.Display(listOfTower[SelectedTowerIndex]);  //Daniel Temporary testing
            //HudManager.GetInstance().Display(tile.CurrentItem);  //Daniel Temporary testing
            Shop.GetInstance().SetPanelActive(Shop.GetInstance().UpgradeSellPanel);
            ShowRange(true);
            return;
        }

        switch (tile.TileType)
        {
            case TileType.Tower:
                //PanelSelection(MenuInteraction.GetInstance().storeIndex);  //Daniel temporary testing
                //HudManager.GetInstance().Display(listOfTower[selectedTowerIndex]);  //Daniel Temporary testing
                Shop.GetInstance().SetPanelActive(Shop.GetInstance().ShopPanel);
                ShowItemOnTile(listOfTowerPlaceHolder[selectedTowerIndex], tile);
                break;
            case TileType.Barrier:
                //PanelSelection(MenuInteraction.GetInstance().storeIndex); //Daniel temporary testing
                //HudManager.GetInstance().Display(listOfBarrier[selectedBarrierIndex]);
                Shop.GetInstance().SetPanelActive(Shop.GetInstance().BarrierPanel);
                ShowItemOnTile(listOfBarrierPlaceHolder[selectedBarrierIndex], tile);
                break;
            default:
                break;
        }
    }

    private void HidePlaceHolders()
    {
        foreach (var item in listOfBarrierPlaceHolder)
        {
            item.SetActive(false);
        }
        foreach (var item in listOfTowerPlaceHolder)
        {
            item.SetActive(false);
        }
    }

    /// <summary>
    /// Show cursor on tile.
    /// </summary>
    /// <param name="cursor">Cursor to show</param>
    /// <param name="tile">Tile to show cursor on</param>
    public void ShowCursorOnTile(GameObject cursor, ItemTile tile)
    {
        if (tile == null)
        {
            //Debug.LogError("TILE IS NULL");
            return;
        }
        cursor.SetActive(true);
        cursor.transform.position = tile.transform.position;
        cursor.transform.rotation = tile.transform.rotation;
        cursor.transform.position += Vector3.up * 3.01f;
    }

    /// <summary>
    /// Show the selected item on the selected tile as preview.
    /// </summary>
    /// <param name="item">Item to show</param>
    /// <param name="tile">Selected Tile</param>
    private void ShowItemOnTile(GameObject item, ItemTile tile)
    {
        if (tile == null)
        {
            //Debug.LogError("TILE IS NULL");
            return;
        }
        item.SetActive(true);
        item.transform.position = tile.transform.position;
        item.transform.rotation = tile.transform.rotation;
        item.transform.position += Vector3.up * 3.0f;
    }

    /// <summary>
    /// Place an item on the selected tile if it is empty.
    /// </summary>
    public void PlaceItem()
    {
        if (selectedTile == null)
        {
            Debug.Log("No Tile Selected.");
            return;
        }
        if (selectedTile.CurrentItem != null)
        {
            UpgradeItem();
            return;
        }

        switch (selectedTile.TileType)
        {
            case TileType.Tower:
                if (!MoneyManager.GetInstance().TryToBuy(listOfTower[selectedTowerIndex].GetComponent<Item>().Value))
                {
                    return;
                }
                InstantiateItemOnTile(listOfTower[selectedTowerIndex]);
                //Shop.GetInstance().SetActiveToolTip(false);
                break;
            case TileType.Barrier:
                if (!MoneyManager.GetInstance().TryToBuy(listOfBarrier[selectedBarrierIndex].GetComponent<Item>().Value))
                {
                    return;
                }
                InstantiateItemOnTile(listOfBarrier[selectedBarrierIndex]);
                //Shop.GetInstance().SetActiveToolTip(false);
                break;
            default:
                break;
        }
        //selectedTile.CurrentItem.name = selectedTile.CurrentItem.GetComponent<Item>().ItemName;
        //TileSelection(selectedTile);
        DeselectTile();
    }

    private void UpgradeItem()
    {
        Item upgradeVersion = selectedTile.CurrentItem.GetComponent<Item>().UpgradeVersion;
        if (upgradeVersion != null)
        {
            if (!MoneyManager.GetInstance().TryToBuy(upgradeVersion.Value))
            {
                Debug.Log("Not enough money for Upgrade!");
                return;
            }
            else
            {
                Debug.Log("Upgrading!");
                Destroy(selectedTile.CurrentItem);
                InstantiateItemOnTile(upgradeVersion.gameObject);
            }
        }
        else
        {
            Debug.Log("No upgrade version, available.");
        }
        DeselectTile();
        //TileSelection(selectedTile);
    }

    /// <summary>
    /// Remove and destroy the Item on the current selected tile, and call the sell method.
    /// </summary>
    public void RemoveItem()
    {
        if (selectedTile == null)
        {
            Debug.Log("No Tile Selected.");
            return;
        }
        if (selectedTile.CurrentItem == null)
        {
            Debug.Log("No Item on the current selected Tile.");
            return;
        }
        MoneyManager.GetInstance().MoneyChange(selectedTile.CurrentItem.GetComponent<Item>().Value); //Sell item
        PlayerManager.GetInstance().RemovePlayer(selectedTile.CurrentItem.GetComponent<Item>());
        Destroy(selectedTile.CurrentItem.gameObject);
        selectedTile.CurrentItem = null;
        Shop.GetInstance().SetPanelActive(Shop.GetInstance().Placeholder);
        //TileSelection(selectedTile);
        DeselectTile();
    }
    
    public void InstantiateItemOnTile(GameObject item)
    {
        selectedTile.CurrentItem =
                    Instantiate(
                        item,
                        selectedTile.transform.position + Vector3.up * 3.0f,
                        selectedTile.transform.rotation,
                        //LevelManager.GetInstance().CurrLvlObj.transform
                        selectedTile.gameObject.transform
                        );
        Item newItem = selectedTile.CurrentItem.GetComponent<Item>();
        newItem.Value /= 2;
        PlayerManager.GetInstance().AddPlayer(newItem);
        HidePlaceHolders();
    }

    public void StoreButtonPressed()
    {
        HidePlaceHolders();
        ShowItemOnTile(listOfTowerPlaceHolder[selectedTowerIndex], selectedTile);
        TileSelection(selectedTile);
    }

    public void SetTowerSelectionIndex(int index)
    {
        selectedTowerIndex = index;
    }

    public void PanelSelection(int index)
    {
        MenuInteraction.GetInstance().PanelToggle(index);
    }

    public void GameOver()
    {
        Pause.GetInstance().PauseGame();
        PanelSelection(gameOverPanelIndex);
    }

    public void ToggleHealthBars()
    {
        showHealthBars = !showHealthBars;
        foreach (var item in EnemyManager.GetInstance().ListOfEnemies)
        {
            item.HealthBar.gameObject.SetActive(showHealthBars);
        }
    }
    

}
