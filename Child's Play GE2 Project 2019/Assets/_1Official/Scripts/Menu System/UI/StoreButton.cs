using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    [SerializeField] public GameObject itemType;
    private Item itemScript;

    private int _myIndex;

    public int MyIndex { get => _myIndex; set => _myIndex = value; }

    // Start is called before the first frame update
    void Start()
    {
        itemScript = itemType.gameObject.GetComponent<Item>();
        _myIndex = itemScript.IndexInGM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
