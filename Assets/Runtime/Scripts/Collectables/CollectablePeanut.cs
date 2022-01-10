using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePeanut : Collectable
{
    public override void PickedUpFeedback(PlayerCollisionInfo collisionInfo)
    {
        collisionInfo.GameMode.IncreasePeanutCount();
    }
}
