using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    moneyIncome,
    onButtonClick,
    onButtonOver,
    placeTower,
    placeBarrier,
    removeTower,
    upgrade,
    selectTile,
    gameOver,
    winCopleted,
    levelCompleted,
    warmupPhase,
    _level01,
    _level02,
    _level03,
    _level04,
    _level05,
    missileSfx
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
        uiSfx.PlayOneShot(GetAudioClip(Sound.onButtonClick), 0.3f);
    }

     public void PlaySoundOneShotShop(Sound s, float vol = 1f)
     {
            if (!uiSfx.isPlaying)
            {
                uiSfx.PlayOneShot(GetAudioClip(s), vol);
            }
     }

    public void PlayMusic(int level)
    {
        switch (level)
        {
            case 0:
                ambientMusic.clip = GetAudioClip(Sound._level01);
                ambientMusic.Play();
                break;
            case 1:
                ambientMusic.clip = GetAudioClip(Sound._level02);
                ambientMusic.Play();
                break;
            case 2:
                ambientMusic.clip = GetAudioClip(Sound._level03);
                ambientMusic.Play();
                break;
            case 3:
                ambientMusic.clip = GetAudioClip(Sound._level04);
                ambientMusic.Play();
                break;
            case 4:
                ambientMusic.clip = GetAudioClip(Sound._level05);
                ambientMusic.Play();
                break;
            default:
                break;
        }
    }

    public void StopMusic()
    {
        ambientMusic.Stop();
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


