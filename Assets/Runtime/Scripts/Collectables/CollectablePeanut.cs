using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePeanut : Collectable
{
    protected override void ExecuteCollectableBehaviour(in PlayerCollisionInfo collisionInfo)
    {
        collisionInfo.GameMode.IncreasePeanutCount();
    }
}
