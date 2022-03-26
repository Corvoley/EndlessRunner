using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Collectable : MonoBehaviour , IPlayerCollisionReact
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject model;

    protected abstract void ExecuteCollectableBehaviour(in PlayerCollisionInfo collisionInfo);
    public void OnPickedUp(PlayerCollisionInfo collisionInfo)
    {
        
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupSound);
        model.SetActive(false);
        Destroy(gameObject, pickupSound.length);
        ExecuteCollectableBehaviour(collisionInfo);
    }    

    public void ReactToPlayerCollision(in PlayerCollisionInfo collisionInfo)
    {
        OnPickedUp(collisionInfo);
    }
}
