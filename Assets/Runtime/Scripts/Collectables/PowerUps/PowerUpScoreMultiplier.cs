using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScoreMultiplier : PowerUp
{
    [SerializeField] private int scoreMultiplier = 2;
    [SerializeField] private float powerUpDuration = 5;

    [SerializeField] private GameObject playerPowerUpParticlesPrefab;

    protected override float LifeTimeAfterPickUp => powerUpDuration * 1.1f;
    public override void PickedUpFeedback(PlayerCollisionInfo collisionInfo)
    {
        StartCoroutine(ScoreMultiplierCor(collisionInfo));
        
    }

    public IEnumerator ScoreMultiplierCor(PlayerCollisionInfo collisionInfo)
    {
        transform.SetParent(null);
        GameObject particle = Instantiate(playerPowerUpParticlesPrefab, collisionInfo.Player.transform);
        collisionInfo.GameMode.TemporaryScoreMultiplier = scoreMultiplier;
        yield return new WaitForSeconds(powerUpDuration);
        collisionInfo.GameMode.TemporaryScoreMultiplier = 1;
        Destroy(particle);
    }
}
