using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Collectable : MonoBehaviour , IPlayerCollisionReact
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject model;
    protected virtual float LifeTimeAfterPickUp => pickupSound.length;

    public void OnPickedUp(in PlayerCollisionInfo collisionInfo)
    {
        PickedUpFeedback(collisionInfo);
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupSound);
        model.SetActive(false);
        Destroy(gameObject, LifeTimeAfterPickUp);
    }

    public abstract void PickedUpFeedback(PlayerCollisionInfo collisionInfo);

    public void ReactToPlayerCollision(in PlayerCollisionInfo collisionInfo)
    {
        OnPickedUp(collisionInfo);

    }
}
