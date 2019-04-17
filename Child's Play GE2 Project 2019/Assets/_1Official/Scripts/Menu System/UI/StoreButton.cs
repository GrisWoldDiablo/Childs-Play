using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Item itemScript;
    [SerializeField] private ButtonType typeOfButton;
    [SerializeField] private GameObject myToolTip;
    private Button thisButton;
    //private Item itemScript;

    private int _myIndex;
    public int MyIndex { get => _myIndex; set => _myIndex = value; }
    public ButtonType TypeOfButton { get => typeOfButton; set => typeOfButton = value; }

    void Start()
    {
        if (thisButton == null)
        {
            thisButton = GetComponent<Button>();
        }
        myToolTip.SetActive(false);
        Shop.GetInstance().TogglePrice(false);
        //itemScript = itemType.gameObject.GetComponent<Item>();
        if (itemScript != null)
        {
            _myIndex = itemScript.IndexInGM; 
        }
    }

    private void OnEnable()
    {
        if (thisButton == null)
        {
            thisButton = GetComponent<Button>();
        }
        Shop.GetInstance().TogglePrice(false);
        myToolTip.SetActive(false);
        thisButton.interactable = true;
        if (typeOfButton == ButtonType.Buy)
        {
            bool found = false;
            foreach (Item item in LevelManager.GetInstance().CurrentLevelInfo.ItemsAvailable)
            {
                if (item == itemScript)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                thisButton.interactable = false;
            }
        }

        if (typeOfButton == ButtonType.Upgrade)
        {
            bool found = false;
            foreach (Item item in LevelManager.GetInstance().CurrentLevelInfo.ItemsAvailable)
            {
                Item itemSelected = GameManager.GetInstance().SelectedItem;
                if (itemSelected != null)
                {
                    Item itemUP = itemSelected.UpgradeVersion;
                    if (itemUP != null && item == itemUP)
                    {
                        found = true;
                        break;
                    }
                }                
            }
            if (!found)
            {
                thisButton.interactable = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (thisButton.interactable)
        {
            myToolTip.SetActive(true);
            Shop.GetInstance().TogglePrice();
            if (typeOfButton == ButtonType.Buy)
            {
                Shop.GetInstance().TowerSelect(_myIndex); 
            }
            if (!Shop.GetInstance().ChangePrice(itemScript, typeOfButton))
            {
                GameManager.GetInstance().DeselectTile();
            }
            SoundManager.GetInstance().PlaySoundOneShot(Sound.onButtonOver, 0.05f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myToolTip.SetActive(false);
        Shop.GetInstance().TogglePrice(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
