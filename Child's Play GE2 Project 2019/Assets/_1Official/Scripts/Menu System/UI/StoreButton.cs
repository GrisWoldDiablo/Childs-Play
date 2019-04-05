using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] public GameObject itemType;
    [SerializeField] private ButtonType typeOfButton;
    [SerializeField] private GameObject myToolTip;

    private Item itemScript;

    private int _myIndex;
    public int MyIndex { get => _myIndex; set => _myIndex = value; }
    public ButtonType TypeOfButton { get => typeOfButton; set => typeOfButton = value; }

    void Start()
    {
        myToolTip.SetActive(false);
        itemScript = itemType.gameObject.GetComponent<Item>();
        _myIndex = itemScript.IndexInGM;
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myToolTip.SetActive(true);
        Shop.GetInstance().TowerSelect(_myIndex);
        Shop.GetInstance().ChangePrice(itemScript, typeOfButton);
        Shop.GetInstance().OnButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myToolTip.SetActive(false);
        Shop.GetInstance().OnButton = false;
    }
}
