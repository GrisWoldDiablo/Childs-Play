using UnityEngine;
using UnityEngine.UI;

public class ReturnSettings : MonoBehaviour {

    [SerializeField] private int confirmationPanelIndex;
    //private MenuInteraction menuCode;
    private Button applyButton;

    public void ExitSettings(int panelIndex)
    {
        if (!applyButton.interactable)
        {
            MenuInteraction.GetInstance().PanelToggle(panelIndex);
        }
        else
        {
            MenuInteraction.GetInstance().PanelToggle(confirmationPanelIndex);
        }
    }

	// Use this for initialization
	void Start () {
        applyButton = GameObject.Find("ApplyButton").GetComponent<Button>();
    }

}
