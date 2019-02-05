using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject itemPrefabTestTower;
    [SerializeField] private GameObject itemPrefabTestPath;
    private GameObject itemSelectedTower;
    private GameObject itemSelectedPath;
    // Start is called before the first frame update
    void Start()
    {
        itemSelectedTower = Instantiate(itemPrefabTestTower);
        itemSelectedTower.GetComponent<Item>().RangeGO.SetActive(true);
        itemSelectedTower.SetActive(false);

        itemSelectedPath = Instantiate(itemPrefabTestPath);
        itemSelectedPath.SetActive(false);
        cam = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        PlaceItemHit();
    }
    // Variable for PlaceItemHit Method
    private ItemTile itemTileScriptForHit;
    private RaycastHit hitForTile;
    /// <summary>
    /// Shoots a ray and detect if the 
    /// </summary>
    private void PlaceItemHit()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitForTile))
        {

            GameObject hitGO = hitForTile.collider.gameObject;
            Debug.Log(hitGO);
            if (hitGO.CompareTag("TileTower"))
            {
                itemTileScriptForHit = hitForTile.collider.GetComponent<ItemTile>();

                if (itemTileScriptForHit.CurrentItem == null)
                {
                    itemSelectedTower.SetActive(true);
                    if (Input.GetButton("Fire1"))
                    {
                        itemTileScriptForHit.CurrentItem = Instantiate(itemPrefabTestTower, hitGO.transform.position + Vector3.up * 3, hitGO.transform.rotation, null);
                    }
                    
                }
                else
                {
                    itemSelectedTower.SetActive(false);
                    if (Input.GetButton("Fire2"))
                    {
                        Destroy(itemTileScriptForHit.CurrentItem);
                        itemTileScriptForHit.CurrentItem = null;
                    }
                }
                itemSelectedTower.transform.position = hitGO.transform.position;
                itemSelectedTower.transform.rotation = hitGO.transform.rotation;
                itemSelectedTower.transform.position += Vector3.up * 3;
            }
            else
            {
                itemSelectedTower.SetActive(false);
            }

            if (hitGO.CompareTag("TilePath"))
            {
                itemTileScriptForHit = hitForTile.collider.GetComponent<ItemTile>();

                if (itemTileScriptForHit.CurrentItem == null)
                {
                    itemSelectedPath.SetActive(true);
                    if (Input.GetButton("Fire1"))
                    {
                        itemTileScriptForHit.CurrentItem = Instantiate(itemSelectedPath, hitGO.transform.position + Vector3.up * 3, hitGO.transform.rotation, null);
                    }

                }
                else
                {
                    itemSelectedPath.SetActive(false);
                    if (Input.GetButton("Fire2"))
                    {
                        Destroy(itemTileScriptForHit.CurrentItem);
                        itemTileScriptForHit.CurrentItem = null;
                    }
                }
                itemSelectedPath.transform.position = hitGO.transform.position;
                itemSelectedPath.transform.rotation = hitGO.transform.rotation;
                itemSelectedPath.transform.position += Vector3.up * 3;
            }
            else
            {
                itemSelectedPath.SetActive(false);
            }
        }
    }
}
