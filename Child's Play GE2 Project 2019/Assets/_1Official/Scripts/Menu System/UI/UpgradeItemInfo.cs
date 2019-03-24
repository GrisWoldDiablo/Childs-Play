using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemInfo : MonoBehaviour
{
    private SelectableInteraction buyUpgradeBtn;
    private GameObject rangeGOUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        buyUpgradeBtn = GetComponent<SelectableInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buyUpgradeBtn.Selected &&
            GameManager.GetInstance().SelectedTile != null &&
            GameManager.GetInstance().SelectedTile.CurrentItem != null &&
            GameManager.GetInstance().SelectedTile.CurrentItem.GetComponent<Item>().UpgradeVersion != null)
        {
            if (rangeGOUpgrade == null)
            {
                rangeGOUpgrade = GameManager.GetInstance().SelectedTile.CurrentItem.GetComponent<Item>().RangeGOUpgrade;
                rangeGOUpgrade.SetActive(true);
            }
        }
        else if (rangeGOUpgrade != null)
        {
            rangeGOUpgrade.SetActive(false);
            rangeGOUpgrade = null;
        }
        
        
    }
}
