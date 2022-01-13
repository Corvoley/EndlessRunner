using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScoreMultiplier : PowerUp
{   
    [SerializeField] private int scoreMultiplier = 2;

    protected override void ActivatePowerUpBehaviour(PlayerController player)
    {
        player.GetComponentInChildren<PowerUpBehaviourScoreMultiplier>().Activate(scoreMultiplier, PowerUpTime);
        
    }


}
