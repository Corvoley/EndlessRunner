using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UiAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip countdownSound;
    [SerializeField] private AudioClip countdownEndSound;

    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayButtonSound()
    {
        Play(buttonSound);
    }
    public void PlayCountdownSound()
    {
        Play(countdownSound);
    }
    public void PlayCountdownEndSound()
    {
        Play(countdownEndSound);
    }
    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }
}
