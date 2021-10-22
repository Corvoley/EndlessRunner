using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float armZ;

    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = player.transform.position;

        currentPosition.z = targetPosition.z + armZ;

        transform.position = currentPosition;
    }


}
