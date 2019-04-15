using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    #region Singleton
    private static HudManager instance = null;

    public static HudManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<HudManager>();
        }
        return instance;
    }
    #endregion
    
    [Header("HUD Components")]
    [SerializeField] private Image _foorFillImage;
    [SerializeField] private Text _foodPercentageTxt;
    [SerializeField] private Text _moneyTxt;
    [SerializeField] private Text _warmUpText;
    [SerializeField] private Text _waveInfoText;
    [SerializeField] private Text _incomingWaveText;
    [SerializeField] private Text _levelNumberText;

    public void UpdateFoodPercentage(float percentage)
    {
        float fill = 1.0f - percentage / 100.0f;
        if (fill < 0)
        {
            fill = 0;
        }
        _foodPercentageTxt.text = $"{fill.ToString("P0")}";
        _foorFillImage.fillAmount = fill;
    }
    
    public void UpdateMoneyAmount(int moneyAmount)
    {
        _moneyTxt.text = $"{moneyAmount}";
    }

    public void UpdateWarmUpText(float time)
    {
        _warmUpText.text = $"Protect your food!\n" +
                           $"Ants Incoming \n" +
                           $"{time.ToString()}s";
    }
    
    public void ShowWarmUpText(bool active)
    {
        _warmUpText.gameObject.SetActive(active);
    }

    public void UpdateWaveInfoText(int wavesLeft, int enemiesLeft)
    {
        _waveInfoText.text = $"{(wavesLeft != 0 ? $"Waves Left: {wavesLeft}\n" : "LAST WAVE!\n")}" +
                             $"Enemies Left: {enemiesLeft}";
    }

    public void UpdateLevelNumberText(int levelNumber)
    {
        _levelNumberText.text = $"LEVEL: {levelNumber}";
    }

    public void UpdateIncomingWaveText(float time)
    {
        if (time <= 0.0f)
        {
            _incomingWaveText.text = "";
        }
        else
        {
            _incomingWaveText.text = $"Incoming wave!\n" +
                                     $"{(int)time}s"; 
        }
    }
}
