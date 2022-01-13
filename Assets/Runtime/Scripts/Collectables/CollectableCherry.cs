using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCherry : Collectable
{

    protected override void ExecuteCollectableBehaviour(in PlayerCollisionInfo collisionInfo)
    {
        collisionInfo.GameMode.IncreaseCherriesCount();
    }


}
