using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IP
{

    [SerializeField] public GameObject itemType;
    [SerializeField] private ButtonType typeOfButton;
    private Item itemScript;

    private int _myIndex;
    public int MyIndex { get => _myIndex; set => _myIndex = value; }
    public ButtonType TypeOfButton { get => typeOfButton; set => typeOfButton = value; }

    void Start()
    {
        itemScript = itemType.gameObject.GetComponent<Item>();
        _myIndex = itemScript.IndexInGM;
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Shop.GetInstance().TowerSelect(_myIndex);
        Shop.GetInstance().SetToolTipText(typeOfButton);
        Shop.GetInstance().SetActiveToolTip(true);
        Shop.GetInstance().MoveToolTip(this.transform.position);
        Shop.GetInstance().ChangePrice(itemScript, typeOfButton);
        Shop.GetInstance().OnButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Shop.GetInstance().OnButton = false;
        Shop.GetInstance().SetActiveToolTip(false);
    }
}
