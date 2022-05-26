using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioFeedback : Feedback
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float volume = 1.0f;
    [SerializeField] private float pitch = 1.0f;
    [SerializeField] private bool randomisePitch = false;
    [SerializeField] private float pitchRandom = 0.05f;

    public override void CompletePreviousFeedback() { }

    public override void CreateFeedback()
    {
        if (randomisePitch)
            PlayClipRandomPitch();
        else
            PlayClip();
    }

    protected void PlayClipRandomPitch()
    {
        var randomPitch = Random.Range(-pitchRandom, pitchRandom);
        var newPitch = pitch + randomPitch;
        SoundManager.Instance.PlaySFX(audioClip, volume, newPitch);
    }


    public void PlayClipRandomPitch(float pitchRange)
    {
        var oldPitchRan = pitchRandom;
        pitchRandom = pitchRange;
        PlayClipRandomPitch();
        pitchRandom = oldPitchRan;
    }

    public void PlayClip()
    {
        SoundManager.Instance.PlaySFX(audioClip, volume, pitch);
    }

    public void PlayClipPitch(float pitch)
    {
        SoundManager.Instance.PlaySFX(audioClip, volume, pitch);
    }
}
