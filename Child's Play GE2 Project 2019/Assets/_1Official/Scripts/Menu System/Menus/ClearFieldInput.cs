using UnityEngine;
using UnityEngine.UI;

public class ClearFieldInput : MonoBehaviour {

    [SerializeField] private InputField inputField;

    public void ClearField()
    {
        inputField.text = string.Empty;
    }

}
