using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableLineSpawner : MonoBehaviour
{
    [Header("Collectables")]
    [SerializeField] private Collectable collectablePrefab;
    [SerializeField] private Collectable rareCollectablePrefab;
    [SerializeField] private float rarePickupChance = 0.1f;
    [Header("Power Ups")]
    [SerializeField] private Collectable[] powerUpPrefabs;    
    [SerializeField] private float powerUpChance = 0.1f;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [Range(1,10)]
    [SerializeField] private float distanceBetweenCollectables = 1f;
    
    
    public void SpawnCollectables(Vector3[] skipPositions)
    {
        if (Random.value < powerUpChance)
        {
            SpawnPowerUp();
        }
        else
        {
            SpawnCollectableLine(skipPositions);
        }
    }
    private void SpawnPowerUp()
    {
        Vector3 currentSpawnPosition = start.position;
        Collectable powerUp = Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length -1)], currentSpawnPosition, Quaternion.identity, transform);
    }

    private void SpawnCollectableLine(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;
        while (currentSpawnPosition.z < end.position.z)
        {
            if (!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                Collectable collectable = ChooseCollectablePrefab() ;
                Instantiate(collectable, currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += distanceBetweenCollectables;
        }
    }
    private Collectable ChooseCollectablePrefab()
    {
        if (Random.value < rarePickupChance)
        {
            return rareCollectablePrefab;
        }
        else
        {
            return collectablePrefab;
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
