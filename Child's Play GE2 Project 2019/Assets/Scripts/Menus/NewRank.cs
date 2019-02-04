using UnityEngine;
using UnityEngine.UI;

public class NewRank : MonoBehaviour
{
    [SerializeField] private Text newRankText;
    [SerializeField] private InputField inputField;
    private Settings settingsCode;

    // Use this for initialization
    void Start()
    {
        settingsCode = GameObject.Find("EventSystem").GetComponent<Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        newRankText.text = "You got the highscore!\nEnter your initial";
    }

    public void SetNewRank()
    {
        if (inputField.text == string.Empty)
        {
            settingsCode.SetLoaderboard();
        }
        else
        {
            settingsCode.SetLoaderboard(inputField.text.ToUpper());
        }
        inputField.text = string.Empty;
    }
}
