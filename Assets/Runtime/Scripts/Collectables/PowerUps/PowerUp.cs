using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : Collectable
{
    [SerializeField] protected float powerUpTime = 10;
    protected float PowerUpTime => powerUpTime;

    protected override sealed void ExecuteCollectableBehaviour(in PlayerCollisionInfo collisionInfo)
    {
        ActivatePowerUpBehaviour(collisionInfo.Player);
    }
    protected abstract void ActivatePowerUpBehaviour(PlayerController player);





}
