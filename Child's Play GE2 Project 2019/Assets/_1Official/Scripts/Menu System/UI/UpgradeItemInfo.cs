using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject rangeGOUpgrade;
    
    /// <summary>
    /// When the pointer (mouse cursor) enters the button field, 
    /// it shows the range for the upgrade version of the currently select item.
    /// </summary>
    /// <param name="eventData">Pointer Data</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.GetInstance().SelectedTile != null &&
            GameManager.GetInstance().SelectedTile.CurrentItem != null &&
            GameManager.GetInstance().SelectedTile.CurrentItem.GetComponent<Item>().UpgradeVersion != null)
        {
            // Insert Code here when mouse is over button and there is upgrade available.
            
            rangeGOUpgrade = GameManager.GetInstance().SelectedTile.CurrentItem.GetComponent<Item>().RangeGOUpgrade;
            rangeGOUpgrade.SetActive(true);
        }
    }

    /// <summary>
    /// When the pointer (mouse cursor) exit the button field, 
    /// it hides the range for the upgrade version of the currently select item.
    /// </summary>
    /// <param name="eventData">Pointer Data</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Insert Code here when mouse exit button to reset any changes made on enter.

        //Hide range upgrade.
        if (rangeGOUpgrade != null)
        {
            rangeGOUpgrade.SetActive(false);
        }
    }
}
