using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#if (UNITY_EDITOR)
[CustomEditor(typeof(ItemTile))]
[CanEditMultipleObjects]
public class ObjectBuilderEditor : Editor
{
    private ItemTile myScript;
    private List<ItemTile> myScripts;
    private void OnEnable()
    {
        myScript = (ItemTile)target;

        myScripts = new List<ItemTile>();
        foreach (var item in targets)
        {
            myScripts.Add((ItemTile)item);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UpdateObject();
    }

    public void UpdateObject()
    {      
        if (myScripts.Count > 1)
        {
            MeshRot90Multiple();
        }
        else
        {
           MeshRot90();     
        }
    }

    private void MeshRot90Multiple()
    {
        foreach (var tiles in myScripts)
        {
            if (tiles.Rot90Degree)
            {
                //Debug.Log("90 Rot");
                tiles.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                //Debug.Log("Zero Rot");
                tiles.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void MeshRot90()
    {
        if (myScript.Rot90Degree)
        {
            //Debug.Log("90 Rot");
            myScript.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            //Debug.Log("Zero Rot");
            myScript.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
#endif

public enum TileType
{
    Tower,
    Barrier,
    Unavailable
}

//[RequireComponent(typeof(TileAutomatic))]
public class ItemTile : MonoBehaviour
{
    [SerializeField] private TileType tileType;
    [SerializeField] private GameObject currentItem;
    public GameObject CurrentItem { get => currentItem; set => currentItem = value; }
    public TileType TileType { get => tileType; }

#if (UNITY_EDITOR)
    [Header("Tile Options")]
    [SerializeField] private bool rot90Degree;
    [SerializeField] private Mesh arrowMesh;
    public bool Rot90Degree { get => rot90Degree; set => rot90Degree = value; }
#endif
    [SerializeField] private GameObject barrierHintGO;

    private void Start()
    {
        if (tileType == TileType.Barrier)
        {
            if (barrierHintGO != null)
            {
                Instantiate(barrierHintGO, this.transform.position + (Vector3.up * 3.0f), this.transform.rotation, this.transform);
            }
        }
    }
    /// <summary>
    /// Called every frame when the cursor is above the tile.
    /// </summary>
    private void OnMouseOver()
    {
        // Check if the pointer is over a UI element, if it is exit method.
        if (IsPointerOverUI())
        {
            return;
        }

        if (Input.GetButton("Select"))
        {
            if (tileType != TileType.Unavailable)
            {
                GameManager.GetInstance().TileSelection(this);
                Shop.GetInstance().MoveToClick();
            }
            //else
            //{
            //    GameManager.GetInstance().DeselectTile();
            //}
        }
    }

    /// <summary>
    /// Called once when the cursor exit the tile.
    /// </summary>
    private void OnMouseExit()
    {
        GameManager.GetInstance().TileSelectionCursor.SetActive(false);
        //clickCounter = 0;
    }
    
    /// <summary>
    /// Called once when the cursor enter the tile.
    /// </summary>
    private void OnMouseEnter()
    {
        // Check if the pointer is over a UI element, if it is exit method.
        if (IsPointerOverUI())
        {
            return;
        }

        if (tileType != TileType.Unavailable)
        {
            GameManager.GetInstance().ShowCursorOnTile(GameManager.GetInstance().TileSelectionCursor, this); 
        }
    }

#if(UNITY_EDITOR)
    void OnDrawGizmos()
    {
        if (tileType == TileType.Barrier)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawMesh(arrowMesh, transform.position + (Vector3.up * 3.0f), transform.rotation); 
        }
    }
#endif

    private bool IsPointerOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
