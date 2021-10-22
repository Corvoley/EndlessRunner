using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{

    private PlayerController player;
    [SerializeField] private Animator animator;
    private ProcessInputs processInputs;

    private void Awake()
    {
        processInputs = GetComponent<ProcessInputs>();
        player = GetComponent<PlayerController>();
        player.enabled = false;
        animator.SetBool(PlayerAnimationConstants.IsIdle, true);
    }
    private void Update()
    {
        AnimationController();
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
    }

    private void AnimationController()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);

        if (processInputs.IsStartKeyDown() && player.IsIdle)
        {
           player.enabled = true;
           animator.SetBool(PlayerAnimationConstants.IsIdle, false);
        }
    }

    public void DeathAnimation()
    {
        animator.SetTrigger(PlayerAnimationConstants.DeathTrigger);
        player.enabled = false;
    }
}
