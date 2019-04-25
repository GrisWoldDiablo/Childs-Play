using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    MoneyIncome,
    OnButtonClick,
    OnButtonOver,
    PlaceTower,
    PlaceBarrier,
    RemoveTower,
    Upgrade,
    SelectTile,
    GameOver,
    WinCompleted,
    LevelCompleted,
    WarmupPhase,
    Music_Level01,
    Music_Level02,
    Music_Level03,
    Music_Level04,
    Music_Level05,
    MissileSfx,
    Spawn,
    Food,
}

[Serializable]
public class SoundAudioclip
{
    [SerializeField] private Sound _sound;
    [SerializeField] private AudioClip _audioClip;

    public Sound Sound { get => _sound; set => _sound = value; }
    public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }
}


public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance = null;

    public static SoundManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<SoundManager>();
        }
        return _instance;
    }
    #endregion

    [SerializeField] private SoundAudioclip[] _mySounds;
    [SerializeField] private AudioSource _ambientMusic;
    [SerializeField] private AudioSource _uiSfx;

    public void PlaySoundOneShot(Sound s , float vol = 1f)
    {
        _uiSfx.PlayOneShot(GetAudioClip(s), vol);
    }

    public void PlaySoundButton()
    {
        _uiSfx.PlayOneShot(GetAudioClip(Sound.OnButtonClick));
    }

     public void PlaySoundOneShotShopOnly(Sound s, float vol = 1f)
     {
            if (!_uiSfx.isPlaying)
            {
                _uiSfx.PlayOneShot(GetAudioClip(s), vol);
            }
     }

    public void PlayMusic(int level)
    {
        switch (level)
        {
            case 0:
                _ambientMusic.clip = GetAudioClip(Sound.Music_Level01);
                _ambientMusic.Play();
                break;
            case 1:
                _ambientMusic.clip = GetAudioClip(Sound.Music_Level02);
                _ambientMusic.Play();
                break;
            case 2:
                _ambientMusic.clip = GetAudioClip(Sound.Music_Level03);
                _ambientMusic.Play();
                break;
            case 3:
                _ambientMusic.clip = GetAudioClip(Sound.Music_Level04);
                _ambientMusic.Play();
                break;
            case 4:
                _ambientMusic.clip = GetAudioClip(Sound.Music_Level05);
                _ambientMusic.Play();
                break;
            default:
                break;
        }
    }

    public void StopMusic()
    {
        _ambientMusic.Stop();
    }


    public AudioClip GetAudioClip(Sound s)
    {
        foreach (SoundAudioclip audioClip in _mySounds)
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


