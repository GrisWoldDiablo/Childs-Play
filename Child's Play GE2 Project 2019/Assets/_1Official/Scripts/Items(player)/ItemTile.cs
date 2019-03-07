using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum TileMesh {
    Side_0,                 // index : 0
    Side_1,                 // index : 1
    Sides_2_Corner,         // index : 2
    Sides_2_OppositeSide,   // index : 3
    Sides_3,                // index : 4
    Sides_4,                // index : 5
    Corner_1,               // index : 6
    Corners_2_SameSide,     // index : 7
    Corners_2_OppositeSide, // index : 8
    Corners_3,              // index : 9
    Corners_4,              // index : 10
    Side_1_Corner_1,        // index : 11
    Sides_2_Corner_1_Corner,// index : 12
    Side_1_Corners_2        // index : 13
}

#if (UNITY_EDITOR)
[CustomEditor(typeof(ItemTile))]
[CanEditMultipleObjects]
public class ObjectBuilderEditor : Editor
{
    private TileMesh _tileMesh;
    private ItemTile myScript;
    private MeshFilter _meshFilter;
    private Mesh[] meshes;
    private void OnEnable()
    {
        myScript = (ItemTile)target;
        _meshFilter = myScript.GetComponent<MeshFilter>();
        meshes = myScript.TileMeshes;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UpdateObject();
    }

    public void UpdateObject()
    {
        _tileMesh = myScript.TileMeshSelected;
        switch (_tileMesh)
        {
            case TileMesh.Side_0:
                _meshFilter.mesh = meshes[0];
                break;
            case TileMesh.Side_1:
                _meshFilter.mesh = meshes[1];
                break;
            case TileMesh.Sides_2_Corner:
                _meshFilter.mesh = meshes[2];
                break;
            case TileMesh.Sides_2_OppositeSide:
                _meshFilter.mesh = meshes[3];
                break;
            case TileMesh.Sides_3:
                _meshFilter.mesh = meshes[4];
                break;
            case TileMesh.Sides_4:
                _meshFilter.mesh = meshes[5];
                break;
            case TileMesh.Corner_1:
                _meshFilter.mesh = meshes[6];
                break;
            case TileMesh.Corners_2_SameSide:
                _meshFilter.mesh = meshes[7];
                break;
            case TileMesh.Corners_2_OppositeSide:
                _meshFilter.mesh = meshes[8];
                break;
            case TileMesh.Corners_3:
                _meshFilter.mesh = meshes[9];
                break;
            case TileMesh.Corners_4:
                _meshFilter.mesh = meshes[10];
                break;
            case TileMesh.Side_1_Corner_1:
                _meshFilter.mesh = meshes[11];
                break;
            case TileMesh.Sides_2_Corner_1_Corner:
                _meshFilter.mesh = meshes[12];
                break;
            case TileMesh.Side_1_Corners_2:
                _meshFilter.mesh = meshes[13];
                break;
            default:
                break;
        }

        if (myScript.MeshFlipX)
        {
            myScript.transform.localScale = new Vector3(-1, 1, myScript.transform.localScale.z);
        }
        else
        {
            myScript.transform.localScale = new Vector3(1, 1, myScript.transform.localScale.z);
        }

        if (myScript.MeshFlipZ)
        {
            myScript.transform.localScale = new Vector3(myScript.transform.localScale.x, 1, -1);
        }
        else
        {
            myScript.transform.localScale = new Vector3(myScript.transform.localScale.x, 1, 1);
        }

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

public class ItemTile : MonoBehaviour
{
    [SerializeField] private TileType tileType;
    [SerializeField] private GameObject currentItem;
    public GameObject CurrentItem { get => currentItem; set => currentItem = value; }
    public TileType TileType { get => tileType; }

#if (UNITY_EDITOR)
    [Header("Mesh Selection")]
    [SerializeField] private bool meshFlipX;
    [SerializeField] private bool meshFlipZ;
    [SerializeField] private bool rot90Degree;
    [SerializeField] private TileMesh tileMeshSelected;
    [SerializeField] private Mesh[] tileMeshes;
    [SerializeField] private Mesh arrowMesh;

    public bool MeshFlipX { get => meshFlipX; set => meshFlipX = value; }
    public bool MeshFlipZ { get => meshFlipZ; set => meshFlipZ = value; }
    public bool Rot90Degree { get => rot90Degree; set => rot90Degree = value; }
    public TileMesh TileMeshSelected { get => tileMeshSelected; set => tileMeshSelected = value; }
    public Mesh[] TileMeshes { get => tileMeshes; set => tileMeshes = value; }

#endif

    //private GameManager alexGMTestCode;
    
    // Start is called before the first frame update
    void Start()
    {
        //alexGMTestCode = GameObject.Find("AlexGMTest").GetComponent<GameManager>();
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
        // Check if the pointer is over a UI element, if it is exit method.
        if (IsPointerOverUI())
        {
            return;
        }
        
        if (Input.GetButton("Fire1"))
        {
            if (tileType != TileType.Unavailable)
            {
                GameManager.GetInstance().TileSelection(this);
            }
            else
            {
                GameManager.GetInstance().DeselectTile();
            }
        }
    }

    /// <summary>
    /// Called once when the cursor exit the tile.
    /// </summary>
    private void OnMouseExit()
    {
        GameManager.GetInstance().TileSelectionCursor.SetActive(false);
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
