using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour , IPlayerCollisionReact
{
    [SerializeField] private DecorationSpawner[] decorationSpawners;

    private List<ObstacleDecoration> obstacleDecorations = new List<ObstacleDecoration>();
    public void SpawnDecorations()
    {
        foreach (var decorationSpawner in decorationSpawners)
        {
            decorationSpawner.SpawnDecorations();
            ObstacleDecoration obstacleDecoration = decorationSpawner.CurrentDecoration.GetComponent<ObstacleDecoration>();
            if (obstacleDecoration != null)
            {
                obstacleDecorations.Add(obstacleDecoration);
            }
        }
    }

    public virtual void Die(Collider collider)
    {
        ObstacleDecoration decorationHit = FindDecorationForCollider(collider);
        if (decorationHit != null)
        {
            decorationHit.PlayCollisionFeedback();
        }
    }

    private ObstacleDecoration FindDecorationForCollider(Collider collider)
    {
        float minDisX = Mathf.Infinity;
        ObstacleDecoration minDistDecoration = null;
        foreach (ObstacleDecoration decoration in obstacleDecorations)
        {
            float decorationPosX = decoration.transform.position.x;
            float colliderPosX = collider.bounds.center.x;
            float distX = Mathf.Abs(decorationPosX - colliderPosX);
            if (distX < minDisX)
            {
                minDisX = distX;
                minDistDecoration = decoration;
            }
        }

        return minDistDecoration;
    }

    public void ReactToPlayerCollision(in PlayerCollisionInfo collisionInfo)
    {
        Die(collisionInfo.MyCollider);
        collisionInfo.Player.Die();
        collisionInfo.PlayerAnimationController.Die();
        collisionInfo.GameMode.OnGameOver();
    }
}
