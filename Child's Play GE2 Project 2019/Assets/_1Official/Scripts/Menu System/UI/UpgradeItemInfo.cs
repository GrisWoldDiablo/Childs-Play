using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject rangeGOUpgrade;
       
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.GetInstance().SelectedTile != null &&
            GameManager.GetInstance().SelectedTile.CurrentItem != null &&
            GameManager.GetInstance().SelectedTile.CurrentItem.GetComponent<Item>().UpgradeVersion != null)
        {
            // Insert Code here when mouse is over button and there is upgrade available.

            // Show range upgrade.
            if (rangeGOUpgrade == null)
            {
                rangeGOUpgrade = GameManager.GetInstance().SelectedTile.CurrentItem.GetComponent<Item>().RangeGOUpgrade;
                rangeGOUpgrade.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Insert Code here when mouse exit button to reset any changes made on enter.

        //Hide range upgrade.
        if (rangeGOUpgrade != null)
        {
            rangeGOUpgrade.SetActive(false);
            rangeGOUpgrade = null;
        }
    }
}
