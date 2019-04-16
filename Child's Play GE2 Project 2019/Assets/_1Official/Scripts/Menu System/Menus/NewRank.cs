using UnityEngine;
using UnityEngine.UI;

public class NewRank : MonoBehaviour
{
    [SerializeField] private Text newRankText;
    [SerializeField] private InputField inputField;
    //private Settings settingsCode;

    // Update is called once per frame
    void Update()
    {
        newRankText.text = "You got the highscore!\nEnter your initials";
    }

    public void SetNewRank()
    {
        if (inputField.text == string.Empty)
        {
            Settings.GetInstance().SetLoaderboard();
        }
        else
        {
            Settings.GetInstance().SetLoaderboard(inputField.text.ToUpper());
        }
        inputField.text = string.Empty;
    }
}
