using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerAnimationController animator;

    private void Awake()
    {
        animator = GetComponent<PlayerAnimationController>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            animator.DeathAnimation();
        }
    }



}
