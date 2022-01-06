using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Collectable : MonoBehaviour , IPlayerCollisionReact
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject model;

    public void OnPickedUp(in PlayerCollisionInfo collisionInfo)
    {
        collisionInfo.GameMode.IncreaseCherriesCount();
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupSound);
        model.SetActive(false);
        Destroy(gameObject, pickupSound.length);
    }

    public void ReactToPlayerCollision(in PlayerCollisionInfo collisionInfo)
    {
        OnPickedUp(collisionInfo);

    }
}
