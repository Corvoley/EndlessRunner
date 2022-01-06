using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovable : Obstacle
{
    [SerializeField] private float laneDistanceX = 2;
    [SerializeField] private float speed = 10;

    private float positionT = 0;
    public float LaneDistanceX => laneDistanceX;
    public float MoveSpeed => speed;

    public float SideToSideMoveTime => 1.0f / MoveSpeed;

    private void Update()
    {
        positionT += Time.deltaTime * MoveSpeed;
        float lanePositionX = (Mathf.PingPong(positionT, 1) - 0.5f) * laneDistanceX * 2;

        Vector3 pos = transform.position;
        pos.x = lanePositionX;
        transform.position = pos;
    }

    public override void Die(Collider collider)
    {
        base.Die(collider);
        enabled = false;
    }

    
}
