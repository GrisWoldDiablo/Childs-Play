using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        Shop.GetInstance().ChangePrice(itemScript.Value.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Shop.GetInstance().SetActiveToolTip(false);
    }
}
