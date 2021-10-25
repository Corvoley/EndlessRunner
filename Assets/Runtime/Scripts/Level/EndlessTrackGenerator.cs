using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private TrackSegment[] segmentPrefabs;
    [SerializeField] private TrackSegment firstTrackPrefab;
    private List<TrackSegment> currentSegments = new List<TrackSegment>();

    [SerializeField] private int minTracksInFrontOfPlayer = 3;
    [SerializeField] private float minDistanceToConsiderInsideTrack = 3;

    [SerializeField] private int minTracksForSpawnHard = 10;
    [SerializeField] private int minSpawnChanceTrackHard = 20;  

    [SerializeField] private int minHardTracksForSpawnReward = 1;
    [SerializeField] private int maxHardTracksForSpawnReward = 3;
    private int totalTrackCount = 0;
    private int totalHardTrackCount = 0;


    private void Start()
    {
        SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }
    private void Update()
    {
        SpawnTrackMenager();
    }
    private void SpawnTrackMenager()
    {
        int playerTrackIndex = FindTrackIndexWithPlayer();
        if (playerTrackIndex < 0)
        {
            //TODO: Throw error
            return;
        }
        InstantiateTracksInFrontOfPlayer(playerTrackIndex);
        RemoveTracksBehindPlayer(playerTrackIndex);
    }
    private void SpawnTracks(int trackCount)
    {
        
        TrackSegment previousTrack = currentSegments.Count > 0
            ? currentSegments[currentSegments.Count - 1]
            : null;
        
        for (int i = 0; i < trackCount; i++)
        {
            int percent = Random.Range(1, 101);
            Debug.Log(percent);
            int totalHardTrackCheck = Random.Range(minHardTracksForSpawnReward, maxHardTracksForSpawnReward+1);
            TrackSegment track;
            if (totalTrackCount >= minTracksForSpawnHard)
            {                  
                if (totalHardTrackCount >= totalHardTrackCheck)
                {
                    track = segmentPrefabs[2];
                    previousTrack = SpawnTrackSegment(track, previousTrack);
                    totalHardTrackCount = 0;
                }else if (percent <= minSpawnChanceTrackHard)
                {
                    track = segmentPrefabs[1];
                    previousTrack = SpawnTrackSegment(track, previousTrack);
                    totalHardTrackCount++;                    
                }
                else
                {
                    track = segmentPrefabs[0];
                    previousTrack = SpawnTrackSegment(track, previousTrack);
                }
            }
            else
            {
                track = segmentPrefabs[0];
                previousTrack = SpawnTrackSegment(track, previousTrack);
            }                      
            totalTrackCount++;
        }
    }
    private TrackSegment SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {
        TrackSegment trackInstance = Instantiate(track, transform);
        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.End.position
            + (trackInstance.transform.position - trackInstance.Start.position);
        }
        else
        {
            trackInstance.transform.localPosition = Vector3.zero;
        }
        currentSegments.Add(trackInstance);
        return trackInstance;
    }

    private void InstantiateTracksInFrontOfPlayer(int playerTrackIndex)
    {
        int tracksInFrontOfPlayer = currentSegments.Count - (playerTrackIndex - 1);
        if (tracksInFrontOfPlayer < minTracksInFrontOfPlayer)
        {
            SpawnTracks(minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }
    }
    private void RemoveTracksBehindPlayer(int playerTrackIndex)
    {
        for (int i = 0; i < playerTrackIndex; i++)
        {
            TrackSegment track = currentSegments[i];
            Destroy(track.gameObject);
        }
        currentSegments.RemoveRange(0, playerTrackIndex);
    }
    private int FindTrackIndexWithPlayer()
    {
        int playerTrackIndex = -1;
        for (int i = 0; i < currentSegments.Count; i++)
        {
            TrackSegment track = currentSegments[i];
            if (player.transform.position.z >= (track.Start.position.z + minDistanceToConsiderInsideTrack)
                && player.transform.position.z <= track.End.position.z)
            {
                playerTrackIndex = i;
                break;
            }
        }

        return playerTrackIndex;
    }
}
