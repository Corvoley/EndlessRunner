using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstacles;

    private void Start()
    {
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform);
    }
}
