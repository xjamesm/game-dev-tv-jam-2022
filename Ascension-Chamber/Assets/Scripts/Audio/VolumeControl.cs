using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider masterVol;
    [SerializeField] private Slider musicVol;
    [SerializeField] private Slider sfxVol;

    private void Start()
    {
        InitialiseSliders();
    }

    private void OnEnable()
    {
        masterVol.onValueChanged.AddListener(_ => OnMasterChange());
        musicVol.onValueChanged.AddListener(_ => OnMusicChange());
        sfxVol.onValueChanged.AddListener(_ => OnSFXChange());
    }

    private void OnDisable()
    {
        masterVol.onValueChanged.RemoveAllListeners();
        musicVol.onValueChanged.RemoveAllListeners();
        sfxVol.onValueChanged.RemoveAllListeners();
    }

    private void InitialiseSliders()
    {
        masterVol.value = SoundManager.Instance.GetAudioLevel(SoundManager.masterPrefs);
        musicVol.value = SoundManager.Instance.GetAudioLevel(SoundManager.musicPrefs);
        sfxVol.value = SoundManager.Instance.GetAudioLevel(SoundManager.sfxPrefs);
    }

    public void OnMasterChange()
    {
        SoundManager.Instance.SetAudioLevel(SoundManager.masterPrefs, masterVol.value);
    }

    public void OnMusicChange()
    {
        SoundManager.Instance.SetAudioLevel(SoundManager.musicPrefs, musicVol.value);
    }

    public void OnSFXChange()
    {
        SoundManager.Instance.SetAudioLevel(SoundManager.sfxPrefs, sfxVol.value);
    }
}
