using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDecorationMovable : ObstacleDecoration
{
    [SerializeField] private Animator animator;

    public override void PlayCollisionFeedback()
    {
        base.PlayCollisionFeedback();
        animator.SetTrigger(ObstacleAnimationConstants.Death);
    }
}
