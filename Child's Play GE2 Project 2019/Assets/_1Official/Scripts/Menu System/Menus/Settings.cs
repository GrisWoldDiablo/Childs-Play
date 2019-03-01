

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerSnapshot inMenuSS;
    public AudioMixerSnapshot InMenuSS { get { return inMenuSS; } }
    [SerializeField] private AudioMixerSnapshot inNormalSS;
    public AudioMixerSnapshot InNormalSS { get { return inNormalSS; } }
    [SerializeField] private AudioMixerSnapshot inWaterSS;
    public AudioMixerSnapshot InWaterSS { get { return inWaterSS; } }
    [SerializeField] private string masterVolParam;
    public string MasterVolParam { get { return masterVolParam; } }
    [SerializeField] private string musicVolParam;
    public string MusicVolParam { get { return musicVolParam; } }
    [SerializeField] private string sFXVolParam;
    public string SFXVolParam { get { return sFXVolParam; } }
    [SerializeField] private string sensitivityHParam;
    public string SensitivityHParam { get { return sensitivityHParam; } }
    [SerializeField] private string sensitivityVParam;
    public string SensitivityVParam { get { return sensitivityVParam; } }

    [Header("Setting Panel Interactables")]
    [SerializeField] private SetVolume masterVol;
    [SerializeField] private SetVolume musicVol;
    [SerializeField] private SetVolume sfxVol;
    [SerializeField] private SetGFX qualitySetting;
    [SerializeField] private SetSensitivity sensitivityHLevel;
    [SerializeField] private SetSensitivity sensitivityVLevel;
    [SerializeField] private Button applyButton;
    public Button ApplyButton { get { return applyButton; } }

    [Header("Leaderboard")]
    [SerializeField] private Text rankedText;
    [SerializeField] private Text lbScoreText;
    [SerializeField] private Text lbNameText;
    private List<int> leaderboardScores;
    public List<int> LeaderboardScores { get { return leaderboardScores; } set { leaderboardScores = value; } }
    private List<string> leaderboardNames;
    public List<string> LeaderboardNames { get { return leaderboardNames; } set { leaderboardNames = value; } }
    private int score;
    private int currentLevel;
    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }

    private float masterVolValue;
    public float MasterVolValue { get { return masterVolValue; } set { masterVolValue = value; } }
    private float musicVolValue;
    public float MusicVolValue { get { return musicVolValue; } set { musicVolValue = value; } }
    private float sFXVolValue;
    public float SFXVolValue { get { return sFXVolValue; } set { sFXVolValue = value; } }
    private float sensitivityH;
    public float SensitivityH { get { return sensitivityH; } set { sensitivityH = value; } }
    private float sensitivityV;
    public float SensitivityV { get { return sensitivityV; } set { sensitivityV = value; } }

    [Header("Game Specific")]
    [SerializeField] private int MAXLEVEL = 10;


    private void Awake()
    {
        // Load settings
        masterVolValue = PlayerPrefs.GetFloat(masterVolParam, 0);
        audioMixer.SetFloat(masterVolParam, masterVolValue);
        musicVolValue = PlayerPrefs.GetFloat(musicVolParam, 0);
        audioMixer.SetFloat(musicVolParam, musicVolValue);
        sFXVolValue = PlayerPrefs.GetFloat(sFXVolParam, 0);
        audioMixer.SetFloat(sFXVolParam, sFXVolValue);

        qualitySetting.PrefValue = QualitySettings.GetQualityLevel();
        sensitivityH = PlayerPrefs.GetFloat(sensitivityHParam, 2);
        sensitivityV = PlayerPrefs.GetFloat(sensitivityVParam, 2);
        GetLeaderboard();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveChanges()
    {
        PlayerPrefs.SetFloat(masterVolParam, masterVol.Slider.value);
        PlayerPrefs.SetFloat(musicVolParam, musicVol.Slider.value);
        PlayerPrefs.SetFloat(sFXVolParam,   sfxVol.Slider.value);
        int gfxIndex = (int)Mathf.Floor(qualitySetting.Slider.value);
        QualitySettings.SetQualityLevel(gfxIndex, true);
        PlayerPrefs.SetFloat(sensitivityHLevel.NameParam, sensitivityHLevel.Slider.value);
        sensitivityH = sensitivityHLevel.Slider.value;
        PlayerPrefs.SetFloat(sensitivityVLevel.NameParam, sensitivityVLevel.Slider.value);
        sensitivityV = sensitivityVLevel.Slider.value;
        PlayerPrefs.Save();
        applyButton.interactable = false;
    }

    public void CancelChanges()
    {
        float masterVolValue = PlayerPrefs.GetFloat(masterVolParam, 0);
        masterVol.SetVol(masterVolValue);

        float musicVolValue = PlayerPrefs.GetFloat(musicVolParam, 0);
        musicVol.SetVol(musicVolValue);

        float sfxVolValue = PlayerPrefs.GetFloat(sFXVolParam, 0);
        sfxVol.SetVol(sfxVolValue);

        qualitySetting.SetQuality(qualitySetting.PrefValue);

        float sensitivityHValue = PlayerPrefs.GetFloat(sensitivityHLevel.NameParam, 2);
        sensitivityHLevel.SetSens(sensitivityHValue);
        float sensitivityVValue = PlayerPrefs.GetFloat(sensitivityVLevel.NameParam, 2);
        sensitivityVLevel.SetSens(sensitivityVValue);

        applyButton.interactable = false;

    }

    public void ResetSettings()
    {
        if (masterVol.Slider.value != 0 || musicVol.Slider.value != 0
            || sfxVol.Slider.value != 0 || qualitySetting.Slider.value != 3
            || sensitivityHLevel.Slider.value != 2)
        {
            masterVol.SetVol(0);
            musicVol.SetVol(0);
            sfxVol.SetVol(0);
            qualitySetting.SetQuality(3);
            sensitivityHLevel.SetSens(2);
            sensitivityVLevel.SetSens(2);
        }
    }

    public void GetLeaderboard()
    {
        lbScoreText.text = string.Empty;
        lbNameText.text = string.Empty;
        leaderboardScores = new List<int>();
        for (int i = 0; i < MAXLEVEL; i++)
        {
            leaderboardScores.Add(PlayerPrefs.GetInt("Score" + i, 0));
        }
        leaderboardNames = new List<string>();
        for (int i = 0; i < MAXLEVEL; i++)
        {
            leaderboardNames.Add(PlayerPrefs.GetString("Name" + i, "AAA").ToUpper());
        }
        for (int i = 0; i < MAXLEVEL; i++)
        {
            if (i != 0)
            {
                lbScoreText.text += "\n" + leaderboardScores[i].ToString("D8");
                lbNameText.text += "\n- " + leaderboardNames[i];
            }
            else
            {
                lbScoreText.text += leaderboardScores[i].ToString("D8");
                lbNameText.text += "- " + leaderboardNames[i]; ;
            }
        }

    }

    /// <summary>
    /// Check if the score is higher than what the leaderboard says based on the level
    /// <para>Condition : current score > current high score </para>
    /// </summary>
    /// <param name="score">score to check</param>
    /// <param name="currentLevel">level to verify</param>
    /// <returns>return true if current score is higher than current highscore</returns>
    public bool CheckLeaderboard(int score, int currentLevel)
    {
        this.score = score;
        this.currentLevel = currentLevel;
        return score > leaderboardScores[currentLevel];
    }

    public void SetLoaderboard(string playerName = "NEW")
    {
        rankedText.text = "Newest Highscore on level " + (currentLevel + 1).ToString() + ".";
        leaderboardScores[currentLevel] = score;
        if (currentLevel < MAXLEVEL)
        {
            leaderboardNames[currentLevel] = playerName;
        }
        for (int i = 0; i < MAXLEVEL; i++)
        {
            PlayerPrefs.SetInt("Score" + i, leaderboardScores[i]);
        }
        for (int i = 0; i < MAXLEVEL; i++)
        {
            PlayerPrefs.SetString("Name" + i, leaderboardNames[i].ToUpper());
        }
        PlayerPrefs.Save();
        GetLeaderboard();

    }


}
