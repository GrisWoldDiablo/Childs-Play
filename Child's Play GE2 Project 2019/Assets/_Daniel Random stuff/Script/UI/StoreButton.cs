using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    [SerializeField] public GameObject itemType;
    private Item itemScript;
    private string _name;
    private string _desc;
    private int _cost;
    private int _myIndex;

    public string Name { get => _name; }
    public string Desc { get => _desc;}
    public int Cost { get => _cost;}
    public int MyIndex { get => _myIndex; set => _myIndex = value; }

    // Start is called before the first frame update
    void Start()
    {
        itemScript = itemType.gameObject.GetComponent<Item>();
        _myIndex = itemScript.IndexInGM;
        //_name = itemScript.ItemName;
        //_desc = itemScript.ItemDescription;
        //_cost = itemScript.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
}
