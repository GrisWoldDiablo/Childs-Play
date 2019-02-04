using UnityEngine;
using UnityEngine.UI;



public class SetSensitivity : MonoBehaviour
{
    private enum SensitivityDirection { Horizontal, Vertical }
    private string nameParam;
    public string NameParam { get { return nameParam; } }

    private Button applyButton;
    private float prefValue;
    private Slider slider;
    public Slider Slider { get { return slider; } }
    [SerializeField] private Text valueText;
    [SerializeField] private SensitivityDirection sensDirection;
    private Settings settingsCode;

    public void SetSens(float sliderValue)
    {
        //Debug.Log("SetSens(): " + sliderValue);
        if (prefValue != slider.value)
        {
            //Debug.Log("Turn On Apply Button");
            applyButton.interactable = true;
        }
        slider.value = sliderValue;
        valueText.text = slider.value.ToString("F2");
    }

    // Use this for initialization
    void Start()
    {
        settingsCode = GameObject.FindObjectOfType<Settings>();
        switch (sensDirection)
        {
            case SensitivityDirection.Horizontal:
                nameParam = settingsCode.SensitivityHParam;
                prefValue = settingsCode.SensitivityH;
                break;
            case SensitivityDirection.Vertical:
                nameParam = settingsCode.SensitivityVParam;
                prefValue = settingsCode.SensitivityV;
                break;
            default:
                break;
        }
        
        applyButton = GameObject.Find("ApplyButton").GetComponent<Button>();
        slider = GetComponent<Slider>();
        slider.value = prefValue;
    }

}
