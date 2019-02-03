using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject rangeGO;
    [SerializeField] private string itemName;

    /// <summary>
    /// this is the base value of the Item, when the players buys this item the value is reduce 
    /// but it will go up as this item is upgraded in order to increase its resell value.
    /// </summary>
    [SerializeField] private float value = 1; 

    public GameObject RangeGO { get => rangeGO; set => rangeGO = value; }
    public float Price { get => value; }
    public string ItemName { get => itemName; }

    // Start is called before the first frame update
    void Awake()
    {
        if (rangeGO != null)
        {
            rangeGO.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}