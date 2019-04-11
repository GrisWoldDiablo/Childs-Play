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

    [Header("Selection Cursor Objects")]
    [SerializeField] private GameObject tileSelectionCursor;
    [SerializeField] private GameObject tileSelectedCursor;
    [Header("Item Prefabs")]
    [SerializeField] private GameObject[] listOfTower;
    [SerializeField] private GameObject[] listOfTowerPlaceHolder;
    [SerializeField] private GameObject[] listOfBarrier;
    [SerializeField] private GameObject[] listOfBarrierPlaceHolder;
    
    private int selectedTowerIndex = 0;
    private int selectedBarrierIndex = 0;

    [Header("Panels Indexes")]
    [SerializeField] private int gameOverPanelIndex = 7;
    [SerializeField] private int scorePanelIndex = 8;
    [SerializeField] private int winPanelIndex = 9;
    public int ScorePanelIndex { get => scorePanelIndex; }
    public int WinPanelIndex { get => winPanelIndex; }

    [Header("Game Options")]
    [Header("What are the two Timescale multiplier ")]
    [SerializeField] private float _speedMulOne;
    [SerializeField] private float _speedMulTwo;
    public float SpeedMulOne { get => _speedMulOne; }
    public float SpeedMulTwo { get => _speedMulTwo; }
    private FastForwardButton _fastForwardButton;
    public FastForwardButton FastForwardButton
    {
        get
        {
            if (_fastForwardButton == null)
            {
                _fastForwardButton = GameObject.FindObjectOfType<FastForwardButton>();
            }
            return _fastForwardButton;
        }
    }
    private float currentGameSpeed = 1.0f; 
    public float CurrentGameSpeed { get => currentGameSpeed; set => currentGameSpeed = value; }

    private bool showHealthBars = true;
    public bool ShowHealthBars { get => showHealthBars; }

    private ItemTile selectedTile;
    public ItemTile SelectedTile { get => selectedTile; }
    public GameObject TileSelectionCursor { get => tileSelectionCursor; }

    public Item SelectedItem
    {
        get
        {
            if (SelectedTile.CurrentItem != null)
            {
                return SelectedTile.CurrentItem.GetComponent<Item>();
            }
            return null;
        }
    }

    //public int SelectedTowerIndex { get => selectedTowerIndex; set => selectedTowerIndex = value; }
    //public int SelectedBarrierIndex { get => selectedBarrierIndex; set => selectedBarrierIndex = value; }

    // Start is called before the first frame update
    void Start()
    {
        PlaceHoldersAndCursorsInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && selectedTile != null)
        {
            DeselectTile();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            if (MenuInteraction.GetInstance().AtDefaultOrRootPanel)
            {
                Pause.GetInstance().ToggleMainMenu();
            }
        }
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
        Shop.GetInstance().SetPanelActive(Shop.GetInstance().Placeholder);
    }

    /// <summary>
    /// Hide or show the range of the currently select item as it selects or deselect the tiles
    /// </summary>
    /// <param name="show">showing</param>
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
                if (!show)
                {
                    GameObject rangeGOUpgrade = selectedTile.CurrentItem.GetComponent<Item>().RangeGOUpgrade;
                    if (rangeGOUpgrade != null)
                    {
                        rangeGOUpgrade.SetActive(show);
                    }
                }
                
            }
        }
    }

    /// <summary>
    /// Initialize the prefabs needed for the game.
    /// </summary>
    void PlaceHoldersAndCursorsInit()
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
        if (tile.CurrentItem != null)
        {
            Shop.GetInstance().SetPanelActive(Shop.GetInstance().UpgradeSellPanel);
            ShowRange(true);
            return;
        }

        switch (tile.TileType)
        {
            case TileType.Tower:
                Shop.GetInstance().SetPanelActive(Shop.GetInstance().ShopPanel);
                ShowItemOnTile(listOfTowerPlaceHolder[selectedTowerIndex], tile);
                break;
            case TileType.Barrier:
                Shop.GetInstance().SetPanelActive(Shop.GetInstance().BarrierPanel);
                ShowItemOnTile(listOfBarrierPlaceHolder[selectedBarrierIndex], tile);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Hides all the item's place holders
    /// </summary>
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
                break;
            case TileType.Barrier:
                if (!MoneyManager.GetInstance().TryToBuy(listOfBarrier[selectedBarrierIndex].GetComponent<Item>().Value))
                {
                    return;
                }
                InstantiateItemOnTile(listOfBarrier[selectedBarrierIndex]);
                break;
            default:
                break;
        }

        DeselectTile();
    }

    /// <summary>
    /// Tries to upgrade the select Item.
    /// </summary>
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

        DeselectTile();
    }
    
    /// <summary>
    /// Instantiate the Item passed as a param on the currently selected tile.
    /// </summary>
    /// <param name="item">Item to instantiace</param>
    public void InstantiateItemOnTile(GameObject item)
    {
        selectedTile.CurrentItem =
                    Instantiate(
                        item,
                        selectedTile.transform.position + Vector3.up * 3.0f,
                        selectedTile.transform.rotation,
                        LevelManager.GetInstance().CurrentLevelGO.transform // Childd of the Level
                        );
        Item newItem = selectedTile.CurrentItem.GetComponent<Item>();
        newItem.Value /= 2;
        PlayerManager.GetInstance().AddPlayer(newItem);
        HidePlaceHolders();
    }

    /// <summary>
    /// When a store Button is Pressed this method works its magic
    /// </summary>
    public void StoreButtonPressed()
    {
        HidePlaceHolders();
        ShowItemOnTile(listOfTowerPlaceHolder[selectedTowerIndex], selectedTile);
        TileSelection(selectedTile);
    }

    /// <summary>
    /// Change the selected tower index
    /// </summary>
    /// <param name="index">Index of the tower</param>
    public void SetTowerSelectionIndex(int index)
    {
        selectedTowerIndex = index;
    }

    /// <summary>
    /// Toggles the Cavas panels as per incoming index
    /// </summary>
    /// <param name="index">Panel index to display</param>
    public void PanelSelection(int index)
    {
        MenuInteraction.GetInstance().PanelToggle(index);
    }

    /// <summary>
    /// Display the Game Over screen
    /// </summary>
    public void GameOver()
    {
        Pause.GetInstance().PauseGame();
        PanelSelection(gameOverPanelIndex);
    }

    /// <summary>
    /// Show or Hide the enemies health bars.
    /// </summary>
    public void ToggleHealthBars()
    {
        showHealthBars = !showHealthBars;
        foreach (var item in EnemyManager.GetInstance().ListOfEnemies)
        {
            item.HealthBar.gameObject.SetActive(showHealthBars);
        }
    }
    

}
