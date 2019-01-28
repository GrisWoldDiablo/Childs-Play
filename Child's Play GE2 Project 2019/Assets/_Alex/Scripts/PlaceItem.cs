using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject itemPrefabTEST;
    private GameObject itemSelected;
    // Start is called before the first frame update
    void Start()
    {
        itemSelected = Instantiate(itemPrefabTEST);
        itemSelected.SetActive(false);
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        {

            GameObject hitGO = hit.collider.gameObject;
            Debug.Log(hitGO);
            if (hitGO.CompareTag("Tile"))
            {
                itemSelected.SetActive(true);
                itemSelected.transform.position = hitGO.transform.position;
                if (Input.GetButton("Fire1"))
                {
                    ItemTile itemTileScript = hit.collider.GetComponent<ItemTile>();
                    if (itemTileScript.CurrentItem == null)
                    {
                        itemTileScript.CurrentItem = Instantiate(itemPrefabTEST, hitGO.transform.position, itemPrefabTEST.transform.rotation, null);
                    }

                }
            }
            else
            {
                itemSelected.SetActive(false);
            }
        }
    }
}
