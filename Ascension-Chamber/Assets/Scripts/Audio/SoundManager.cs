using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BGMusicPlaylist
{
    public string sceneName = "";
    public AudioClip audioClip = null;
    [Range(0, 1)] public float volume = 1.0f;
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource bgSource;
    [SerializeField] private BGMusicPlaylist[] musicPlaylist = null;
    [SerializeField] private SFXPool sfxPool;

    public const string masterPrefs = "MasterVolume";
    public const string musicPrefs = "MusicVolume";
    public const string sfxPrefs = "SFXVolume";

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InitialiseAudioLevels();
        SceneManager.activeSceneChanged += OnActiveSceneChange;

        UpdateBGAudio();
    }

    private void OnActiveSceneChange(Scene arg0, Scene arg1)
    {
        InitialiseAudioLevels();
        UpdateBGAudio();
    }

    public void UpdateBGAudio()
    {
        if (musicPlaylist.Length <= 0) return;

        string currentScene = SceneManager.GetActiveScene().name;
        OverrideBGM(currentScene);
    }

    public void OverrideBGM(string playerListName, bool looping = true)
    {
        foreach(BGMusicPlaylist playlist in musicPlaylist)
        {
            if (playlist.sceneName == playerListName)
            {
                if (bgSource.clip == playlist.audioClip)
                    return;
                SetBGM(playlist, looping);
            }
        }
    }

    private void SetBGM(BGMusicPlaylist playlist, bool looping = true)
    {
        bgSource.volume = playlist.volume;
        bgSource.clip = playlist.audioClip;
        bgSource.loop = looping;
        bgSource.Play();
    }

    public void StopAllAudio()
    {
        bgSource.Stop();
    }

    private void InitialiseAudioLevels()
    {
        InitChannel(masterPrefs);
        InitChannel(musicPrefs);
        InitChannel(sfxPrefs);
    }

    private void InitChannel(string audioPrefs)
    {
        float value = 1.0f;

        if(PlayerPrefs.HasKey(audioPrefs))
        {
            value = PlayerPrefs.GetFloat(audioPrefs);
        }
        else
        {
            PlayerPrefs.SetFloat(audioPrefs, value);
        }

        float dbVol = LinearToDB(value);
        mixer.SetFloat(audioPrefs, dbVol);
    }

    public void SetAudioLevel(string audioPrefs, float value)
    {
        float dbValue = LinearToDB(value);
        mixer.SetFloat(audioPrefs, dbValue);
        PlayerPrefs.SetFloat(audioPrefs, value);
    }

    public float GetAudioLevel(string audioPrefs)
    {
        if (!PlayerPrefs.HasKey(audioPrefs))
        {
            Debug.LogError("Trying to get audioPrefs: " + audioPrefs + " but it doesn't exist!");
            return 0f;
        }
        return PlayerPrefs.GetFloat(audioPrefs);
    }

    public float GetAudioLevelDB(string audioPrefs)
    {
        return LinearToDB(GetAudioLevel(audioPrefs));
    }

    public static float LinearToDB(float value)
    {
        float dB = value > 0.0f ? Mathf.Log10(value) * 20.0f : -80.0f;
        return dB;
    }

    public void PlaySFX(AudioClip audioClip, float volume = 1.0f, float pitch = 1.0f)
    {
        if (!audioClip)
            return;

        var sfx = sfxPool.Pool.Get();
        if(sfx != null)
        {
            sfx.pitch = pitch;
            sfx.volume = volume;
            sfx.clip = audioClip;
            sfx.Play();
        }

    }
}
