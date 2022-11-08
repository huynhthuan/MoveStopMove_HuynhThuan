using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    TAP,
    LEVEL_UP,
    DIE,
    WEAPON_FLY,
    INTRO_ITEM,
    REVIVE,
    COMPLETE
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource[] audioSources;

    [SerializeField]
    private AudioMixer audioMixers;

    public void OnInit()
    {
        InitAudioSourcesOutPut();
    }

    private void InitAudioSourcesOutPut()
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.outputAudioMixerGroup = audioMixers.outputAudioMixerGroup;
        }
    }

    public void PlayAudio(AudioType audioKey)
    {
        audioSources[(int)audioKey].Play();
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
