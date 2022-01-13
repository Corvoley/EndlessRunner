using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviourScoreMultiplier : PowerUpBehaviour
{
    [SerializeField] private GameMode gameMode;
    private int scoreMultiplier;

    public void Activate(int multiplier, float duration)
    {
        scoreMultiplier = multiplier;
        ActivateForDuration(duration);
    }
    protected override void EndBehaviour()
    {
        gameMode.TemporaryScoreMultiplier = 1;
    }

    protected override void StartBehaviour()
    {
        gameMode.TemporaryScoreMultiplier = scoreMultiplier;
    }

    protected override void UpdateBehaviour()
    {        
    }
}
