using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;
    
    private void Update()
    {

        transform.Rotate(0, 0, 2, Space.Self);
    }

    public void PlayPickupSound()
    {
        Play(pickupSound);
    }
    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }
}
