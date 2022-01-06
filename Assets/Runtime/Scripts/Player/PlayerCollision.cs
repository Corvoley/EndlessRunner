using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    private PlayerController playerController;
    private PlayerAnimationController animationController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IPlayerCollisionReact collisionReaction = other.GetComponent<IPlayerCollisionReact>();
        if (collisionReaction != null)
        {
            collisionReaction.ReactToPlayerCollision(new PlayerCollisionInfo()
            {
                Player = playerController,
                PlayerAnimationController = animationController,
                GameMode = gameMode,
                MyCollider = other

            });
            ;
        }

    }
}
