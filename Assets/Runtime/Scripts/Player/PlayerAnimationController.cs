using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerController player;

    private void Awake()
    {        
        player = GetComponent<PlayerController>();
        player.PlayerDeathEvent += OnPlayerDeath;
    }

    private void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
        
        
    }
    private void OnPlayerDeath()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }    
    public void PlayIdleAnimation()
    {
        animator.SetTrigger(PlayerAnimationConstants.Restart);
    }

    public IEnumerator PlayStartGameAnimation()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.StartGameAnimationStateName))
        {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.StartGameAnimationStateName) 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }      

    }

    private void OnDestroy()
    {
        player.PlayerDeathEvent -= OnPlayerDeath;
    }
}
