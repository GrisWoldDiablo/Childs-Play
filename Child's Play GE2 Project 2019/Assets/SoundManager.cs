using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    onButtonClick,
    onButtonOver,
    placeTower,
    removeTower,
    upgrade,
    selectTile,
    gameOver,
    winGame,
    scoreScreen,
    warmupPhase,
    _levels1_2,
    _levels3_4,
    _level5
}

[Serializable]
public class SoundAudioclip
{
    [SerializeField] private Sound sound;
    [SerializeField] private AudioClip audioClip;

    public Sound Sound { get => sound; set => sound = value; }
    public AudioClip AudioClip { get => audioClip; set => audioClip = value; }
}


public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance = null;

    public static SoundManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<SoundManager>();
        }
        return instance;
    }
    #endregion

    [SerializeField] private SoundAudioclip[] mySounds;
    [SerializeField] private AudioSource ambientMusic;
    [SerializeField] private AudioSource uiSfx;

    public void PlaySoundOneShot(Sound s , float vol = 1f)
    {
        uiSfx.PlayOneShot(GetAudioClip(s), vol);
    }

    public void PlaySoundButton()
    {
        uiSfx.PlayOneShot(GetAudioClip(Sound.onButtonClick), 0.1f);
    }

    public AudioClip GetAudioClip(Sound s)
    {
        foreach (SoundAudioclip audioClip in mySounds)
        {
            if (audioClip.Sound == s)
            {
                return audioClip.AudioClip;
            }
        }
        Debug.LogError("Sound " + s + " not found");
        return null;
    }



}


