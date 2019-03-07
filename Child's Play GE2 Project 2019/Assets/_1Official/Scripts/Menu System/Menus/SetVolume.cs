using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public enum VolumeTypes { Master, Music, SFX }

[RequireComponent(typeof(Slider))]
public class SetVolume : MonoBehaviour {
    [SerializeField] private VolumeTypes volumeType;
    [SerializeField] private AudioMixer audioMixer;
    private string nameParam;
    //public string NameParam { get { return nameParam; } }

    private float prefValue;
    private Slider slider;
    public Slider Slider { get { return slider; } }
    private Button applyButton;
    //private Settings settingsCode;

    public void SetVol(float sliderValue)
    {
        audioMixer.SetFloat(nameParam, sliderValue);
        if (prefValue != slider.value)
        {
            //Debug.Log("Turn On Apply Button");
            applyButton.interactable = true;
        }
        slider.value = sliderValue;
    }

    // Use this for initialization
    void Start () {
        //Settings.GetInstance() = GameObject.FindObjectOfType<Settings>();
        switch (volumeType)
        {
            case VolumeTypes.Master:
                nameParam = Settings.GetInstance().MasterVolParam;
                prefValue = Settings.GetInstance().MasterVolValue;
                break;
            case VolumeTypes.Music:
                nameParam = Settings.GetInstance().MusicVolParam;
                prefValue = Settings.GetInstance().MusicVolValue;
                break;
            case VolumeTypes.SFX:
                nameParam = Settings.GetInstance().SFXVolParam;
                prefValue = Settings.GetInstance().SFXVolValue;
                break;
            default:
                break;
        }
        applyButton = GameObject.Find("ApplyButton").GetComponent<Button>();
        slider = GetComponent<Slider>();
        //prefValue = PlayerPrefs.GetFloat(nameParam, 0);
        
        slider.value = prefValue;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
