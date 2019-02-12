using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is to be place in the GameManager script
/// </summary>
public class AlexGMTest : MonoBehaviour
{
    [SerializeField] private float currentcash = 5;
    [SerializeField] private GameObject tileSelectionCursor;
    [SerializeField] private GameObject tileSelectedCursor;
    [SerializeField] private GameObject[] listOfTower;
    [SerializeField] private GameObject[] listOfTowerPlaceHolder;
    [SerializeField] private GameObject[] listOfBarrier;
    [SerializeField] private GameObject[] listOfBarrierPlaceHolder;
    [SerializeField] private int selectedTowerIndex = 0;
    [SerializeField] private int selectedBarrierIndex = 0;

    //-//
    // To be placed in UI management script
    [SerializeField] private Text UITextSelectedTile;
    [SerializeField] private Text UITextCash;
    //-//

    private ItemTile selectedTile;
    public ItemTile SelectedTile { get => selectedTile; }
    public GameObject TileSelectionCursor { get => tileSelectionCursor; }

    public int SelectedTowerIndex { get => selectedTowerIndex; set => selectedTowerIndex = value; }
    public int SelectedBarrierIndex { get => selectedBarrierIndex; set => selectedBarrierIndex = value; }

    // Start is called before the first frame update
    void Start()
    {
        ItemSelectionReset(); // for testing
        UpdateCashText(); // to be place in UI management script
        UpdateSelectedTileText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            DeselectTile();
        }
    }

    /// <summary>
    /// Deselect the current selected tile,
    /// hide all placeholder items
    /// and call the method to update the selected tile text.
    /// </summary>
    private void DeselectTile()
    {
        tileSelectedCursor.SetActive(false);

        listOfBarrierPlaceHolder[selectedBarrierIndex].SetActive(false);
        listOfTowerPlaceHolder[selectedTowerIndex].SetActive(false);
        selectedTile = null;
        UpdateSelectedTileText();
    }

    void ItemSelectionReset()
    {
        for (int i = 0; i < listOfBarrierPlaceHolder.Length; i++)
        {
            listOfBarrierPlaceHolder[i] = Instantiate(listOfBarrierPlaceHolder[i]);
            listOfBarrierPlaceHolder[i].SetActive(false);
        }

        for (int i = 0; i < listOfTowerPlaceHolder.Length; i++)
        {
            listOfTowerPlaceHolder[i] = Instantiate(listOfTowerPlaceHolder[i]);
            listOfTowerPlaceHolder[i].SetActive(false);
        }

        tileSelectionCursor.SetActive(false);
        tileSelectedCursor.SetActive(false);

    }

    /// <summary>
    /// Select a tile and sets it as the tile selected.
    /// </summary>
    /// <param name="tile">Tile to select</param>
    public void TileSelection(ItemTile tile)
    {
        listOfBarrierPlaceHolder[selectedBarrierIndex].SetActive(false);
        listOfTowerPlaceHolder[selectedTowerIndex].SetActive(false);
        ShowCursorOnTile(tileSelectedCursor, tile);
        selectedTile = tile;
        UpdateSelectedTileText();
        if (tile.CurrentItem != null)
        {
            return;
        }

        switch (tile.TileType)
        {
            case TileType.Tower:
                ShowItemOnTile(listOfTowerPlaceHolder[selectedTowerIndex], tile);
                break;
            case TileType.Barrier:
                ShowItemOnTile(listOfBarrierPlaceHolder[selectedBarrierIndex], tile);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Show cursor on tile.
    /// </summary>
    /// <param name="cursor">Cursor to show</param>
    /// <param name="tile">Tile to show cursor on</param>
    public void ShowCursorOnTile(GameObject cursor, ItemTile tile)
    {
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
            Debug.Log("Already an Item, remove current Item before.");
            return;
        }

        switch (selectedTile.TileType)
        {
            case TileType.Tower:
                if (!BuyItem(listOfTower[selectedTowerIndex].GetComponent<Item>().Value))
                {
                    return;
                }
                InstantiateItemOnTile(listOfTower[selectedTowerIndex]);
                break;
            case TileType.Barrier:
                if (!BuyItem(listOfBarrier[selectedBarrierIndex].GetComponent<Item>().Value))
                {
                    return;
                }
                InstantiateItemOnTile(listOfBarrier[selectedBarrierIndex]);
                break;
            default:
                break;
        }
        selectedTile.CurrentItem.name = selectedTile.CurrentItem.GetComponent<Item>().ItemName;
        TileSelection(selectedTile);
    }

    /// <summary>
    /// Verify if the player has enough cash to buy the item, 
    /// if he does it removes the value from his cash 
    /// and call the method to update the cash text on the UI.
    /// </summary>
    /// <param name="itemPrice">The value of the Item</param>
    /// <returns></returns>
    private bool BuyItem(float itemPrice)
    {
        if (itemPrice > currentcash)
        {
            Debug.Log("Not Enough Cash.");
            return false;
        }
        currentcash -= itemPrice;
        UpdateCashText();
        return true;
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
        SellItem(selectedTile.CurrentItem.GetComponent<Item>().Value);
        Destroy(selectedTile.CurrentItem.gameObject);
        selectedTile.CurrentItem = null;
        TileSelection(selectedTile);
    }

    /// <summary>
    /// Sell Item, Add item's value it to the player's cash
    /// and call the method to update the cash text on the UI.
    /// </summary>
    /// <param name="itemValue">The value of the Item</param>
    public void SellItem(float itemValue)
    {
        currentcash += itemValue;
        UpdateCashText();
    }

    // to be placed in UI management script
    void UpdateCashText()
    {
        UITextCash.text = $"Current Cash: {currentcash}";
    }

    // to be placed in UI management script
    private void UpdateSelectedTileText()
    {
        if (UITextSelectedTile != null)
        {
            UITextSelectedTile.text = $"Selected Tile: " + (selectedTile != null ? selectedTile.name : "");
            if (selectedTile != null)
            {
                UITextSelectedTile.text += $"\nCurrent Item: " + (selectedTile.CurrentItem != null ? selectedTile.CurrentItem.name : "");
            }
            else
            {
                UITextSelectedTile.text += $"\nCurrent Item: ";
            }
        }
    }
    
    public void InstantiateItemOnTile(GameObject item)
    {
        selectedTile.CurrentItem =
                    Instantiate(
                        item,
                        selectedTile.transform.position + Vector3.up * 3.0f,
                        selectedTile.transform.rotation,
                        selectedTile.transform
                        );
        listOfBarrierPlaceHolder[SelectedBarrierIndex].SetActive(false);
        listOfTowerPlaceHolder[SelectedTowerIndex].SetActive(false);
    }
}
