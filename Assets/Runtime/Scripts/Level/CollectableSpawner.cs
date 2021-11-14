using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private Collectable collectable;
    [Header("Collectables Parameters")]
    [SerializeField] private float chanceToSpawnCollectable = 0.5f;
    [SerializeField] private int minCollactablesCount = 1;
    [SerializeField] private int maxCollectablesCount = 5;
    [SerializeField] private float distanceBetweenCollectables = 1f;

    private void Awake()
    {
        CollectableMenager();
    }
    private void CollectableMenager()
    {
        if (Random.Range(1, 101) <= chanceToSpawnCollectable)
        {
            Vector3 position;
            switch (Random.Range(1, 4))
            {

                case 1:
                    position = new Vector3(0, 0, 0);
                    distanceBetweenCollectables = 1;
                    for (int i = 0; i < Random.Range(minCollactablesCount, maxCollectablesCount + 1); i++)
                    {
                        position.z += distanceBetweenCollectables;
                        Instantiate(collectable, position, Quaternion.Euler(-89.98f, 0, 0), transform);
                        distanceBetweenCollectables++;
                    }
                    break;
                case 2:
                    position = new Vector3(-1.5f, 0, 0);
                    distanceBetweenCollectables = 1;
                    for (int i = 0; i < Random.Range(minCollactablesCount, maxCollectablesCount + 1); i++)
                    {
                        position.z += distanceBetweenCollectables;
                        Instantiate(collectable, position, Quaternion.Euler(-89.98f, 0, 0), transform);
                        distanceBetweenCollectables++;
                    }
                    break;
                case 3:
                    position = new Vector3(1.5f, 0, 0);
                    distanceBetweenCollectables = 1;
                    for (int i = 0; i < Random.Range(minCollactablesCount, maxCollectablesCount + 1); i++)
                    {
                        position.z += distanceBetweenCollectables;
                        Instantiate(collectable, position, Quaternion.Euler(-89.98f, 0, 0), transform);
                        distanceBetweenCollectables++;
                    }

                    break;
            }


        }
    }
}
