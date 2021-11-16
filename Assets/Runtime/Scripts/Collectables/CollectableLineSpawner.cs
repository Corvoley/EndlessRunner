using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableLineSpawner : MonoBehaviour
{
    [SerializeField] private Collectable collectablePrefab;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [Range(1,10)]
    [SerializeField] private float distanceBetweenCollectables = 1f;  
    
    public void SpawnCollectableLine(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;
        while (currentSpawnPosition.z < end.position.z)
        {
            if (!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                Collectable collectable = Instantiate(collectablePrefab, currentSpawnPosition, Quaternion.identity, transform);
            }            
            currentSpawnPosition.z += distanceBetweenCollectables;
        }
    }

    private bool ShouldSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipPositions) 
    { 
        foreach (var skipPosition in skipPositions)
        {
            float skipStart = skipPosition.z - distanceBetweenCollectables * 0.5f;
            float skipEnd = skipPosition.z + distanceBetweenCollectables * 0.5f;

            if (currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Vector3 currentSpawnPosition = start.position;
        while (currentSpawnPosition.z < end.position.z)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(currentSpawnPosition, Vector3.one);
            currentSpawnPosition.z += distanceBetweenCollectables;
        }
    }


}
