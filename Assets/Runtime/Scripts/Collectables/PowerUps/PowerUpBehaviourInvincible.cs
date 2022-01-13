using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviourInvincible : PowerUpBehaviour
{

    public void Activate(float duration)
    {
        ActivateForDuration(duration);
    }
    protected override void EndBehaviour()
    {
    }

    protected override void StartBehaviour()
    {
    }

    protected override void UpdateBehaviour()
    {
    }
}
