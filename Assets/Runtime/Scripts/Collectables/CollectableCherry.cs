using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCherry : Collectable
{

    public override void PickedUpFeedback(PlayerCollisionInfo collisionInfo)
    {
        collisionInfo.GameMode.IncreaseCherriesCount();
    }


}
