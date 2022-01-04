using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideToSideAnimationState : StateMachineBehaviour
{
    private ObstacleMovable obstacle;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo runClipInfo = clips[0];
            //TODO: Assumindo onde o animator esta no objeto
            obstacle = animator.transform.parent.parent.parent.GetComponent<ObstacleMovable>();

            float timeToCompleteAnimationCycle = obstacle.SideToSideMoveTime * 2;

            float speedMultiplier = runClipInfo.clip.length / timeToCompleteAnimationCycle;

            animator.SetFloat(ObstacleAnimationConstants.SideToSideMultiplier, speedMultiplier);
        }
    }    
}
