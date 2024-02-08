using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSystem : Singleton<AudioSystem>
{
    public AudioSource audioSrc;

    [SerializedDictionary] public SerializedDictionary<string, AudioClip> audioDict;

    public override void Awake()
    {
        base.Awake();
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayOnce(AudioClip _audioClip)
    {
        audioSrc.PlayOneShot(_audioClip);
    }

    public void PlayOnce(string _audioName)
    {
        audioSrc.PlayOneShot(audioDict[_audioName]);
        
    }

    public void StopAll()
    {
        audioSrc.Stop();
    }
}

public sealed class SoundEfx
{
    public const string BUTTON_ENTER = "ButtonEnter";
    public const string BUTTON_PRESS = "ButtonPress";
    // public static readonly string 
}

public sealed class Music
{
    public const string BACKGROUND_MENU = "MenuBGM";
    public const string BACKGROUND_INGAME = "IngameBGM";
}
