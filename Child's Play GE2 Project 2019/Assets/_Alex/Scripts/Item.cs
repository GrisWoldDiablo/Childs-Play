using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject rangeGO;

    public GameObject RangeGO { get => rangeGO; set => rangeGO = value; }

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
