using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Tower,
    Barrier,
    Unavailable
}

public class ItemTile : MonoBehaviour
{
    [SerializeField] private TileType tileType;
    [SerializeField] private GameObject currentItem;

    public GameObject CurrentItem { get => currentItem; set => currentItem = value; }
    public TileType TileType { get => tileType; }

    private AlexGMTest alexGMTestCode;
    
    // Start is called before the first frame update
    void Start()
    {
        alexGMTestCode = GameObject.Find("AlexGMTest").GetComponent<AlexGMTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called every frame when the cursor is above the tile.
    /// </summary>
    private void OnMouseOver()
    {
        Debug.Log(this.gameObject.name);
        if (Input.GetButton("Fire1") && tileType != TileType.Unavailable)
        {
            alexGMTestCode.TileSelection(this);
        }
    }

    /// <summary>
    /// Called once when the cursor exit the tile.
    /// </summary>
    private void OnMouseExit()
    {
        alexGMTestCode.TileSelectionCursor.SetActive(false);
    }

    /// <summary>
    /// Called once when the cursor enter the tile.
    /// </summary>
    private void OnMouseEnter()
    {
        if (tileType != TileType.Unavailable)
        {
            alexGMTestCode.ShowCursorOnTile(alexGMTestCode.TileSelectionCursor, this); 
        }
    }
}
