using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Player
{
    [SerializeField] private GameObject rangeGO;
    [SerializeField] private string itemName;
    [SerializeField, Multiline] private string itemDescription;
    [SerializeField] private int indexInGM;
    


    /// <summary>
    /// this is the base value of the Item, when the players buys this item the value is reduce 
    /// but it will go up as this item is upgraded in order to increase its resell value.
    /// </summary>
    [SerializeField] private int value = 1;

    public GameObject RangeGO { get => rangeGO; set => rangeGO = value; }
    public int Value { get => value; set => this.value = value; }
    public string ItemName { get => itemName; }
    public string ItemDescription { get => itemDescription; private set => itemDescription = value; }
    public int IndexInGM { get => indexInGM; private set => indexInGM = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if (rangeGO != null)
        {
            rangeGO.SetActive(false);
        }

        indexInGM = 0;
        //itemDescription = "This is a placeholder description";
    }
}