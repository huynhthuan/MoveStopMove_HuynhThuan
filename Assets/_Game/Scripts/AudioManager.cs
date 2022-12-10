using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public enum AudioType
{
    TAP,
    LEVEL_UP,
    DIE,
    WEAPON_FLY,
    LOSE,
    WIN,
    HIT,
    BACKGROUND
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private List<AudioClipItem> audioClipItems = new List<AudioClipItem>();

    [SerializeField]
    private AudioSource audioSourceFX;


    [SerializeField]
    private AudioSource audioSourceBG;

    [SerializeField]
    private AudioMixer audioMixers;

    public void OnInit()
    {
        Debug.Log("Init audio");
        InitAudioSourcesOutPut();
    }

    private void InitAudioSourcesOutPut()
    {

    }

    public void PlayAudio(AudioType audioType)
    {
        AudioClipItem audioTarget = audioClipItems.Find(audio => audio.audioType == audioType);
        audioSourceFX.clip = audioTarget.audioClip;
        audioSourceFX.Play();
    }

    public void PlayAudioBackground(AudioType audioType)
    {
        if (audioSourceBG.clip != null)
        {
            audioSourceBG.Pause();
        }
        AudioClipItem audioTarget = audioClipItems.Find(audio => audio.audioType == audioType);
        audioSourceBG.clip = audioTarget.audioClip;
        audioSourceBG.Play();
    }

    public void MuteAudio()
    {
        audioMixers.SetFloat("volume", -80f);
    }

    public void UnMuteAudio()
    {
        audioMixers.SetFloat("volume", 0f);
    }
}

[Serializable]
public class AudioClipItem : IEquatable<AudioClipItem>
{
    public AudioType audioType;
    public AudioClip audioClip;

    public bool Equals(AudioClipItem other)
    {
        if (other == null) return false;
        return (this.audioType.Equals(other.audioType));
    }

}