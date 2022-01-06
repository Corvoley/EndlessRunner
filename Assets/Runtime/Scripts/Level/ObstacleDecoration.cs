using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip collisionAudio;
    
    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;


    public virtual void PlayCollisionFeedback()
    {
        AudioUtility.PlayAudioCue(AudioSource, collisionAudio);
        Animation collisionAnimation = GetComponentInChildren<Animation>();
        if (collisionAnimation != null)
        {
            collisionAnimation.Play();
        }

    }

}
