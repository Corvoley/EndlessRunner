using Unity.VisualScripting;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private ObstacleSpawner[] obstacleSpawners;
    [SerializeField] DecorationSpawner decorationSpawner;


    [Header("Collectable")]
    [Range (0,1)]
    [SerializeField] private float collectableSpawnChance = 0.5f;
    [SerializeField] private CollectableLineSpawner[] collectableLineSpawners;

    public Transform Start => start;
    public Transform End => end;

    public float Length => Vector3.Distance(End.position, Start.position);
    public float SqrLength => (End.position - Start.position).sqrMagnitude;

    public ObstacleSpawner[] ObstacleSpawners => obstacleSpawners;
    public DecorationSpawner DecorationSpawner => decorationSpawner;


    public void SpawnCollectables()
    {
        if (collectableLineSpawners.Length > 0 && Random.value <= collectableSpawnChance)
        {
            Vector3[] skipPositions = new Vector3[obstacleSpawners.Length];
            for (int i = 0; i < skipPositions.Length; i++)
            {
                skipPositions[i] = obstacleSpawners[i].transform.position;
            }


            int randomIndex = Random.Range(0, collectableLineSpawners.Length);
            CollectableLineSpawner collectableSpawner = collectableLineSpawners[randomIndex];
            collectableSpawner.SpawnCollectables(skipPositions);
        }
    }


}
