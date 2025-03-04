using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private Transform bossSpawnPoint;
    public int keyIndex;
    public RoomManager roomManager;

    public List<Transform> GetRandomSpawnPoints(float percentage)
    {
        int numberOfPoints = Mathf.CeilToInt(percentage * spawnPoints.Count);

        List<SpawnPoint> randomPoints = new List<SpawnPoint>(spawnPoints);
        List<SpawnPoint> selectedPoints = new List<SpawnPoint>();

        

        for (int i = 0; i < numberOfPoints; i++)
        {
            int index = Random.Range(0, randomPoints.Count);
            selectedPoints.Add(randomPoints[index]);
            randomPoints.RemoveAt(index);
        }

        keyIndex = Random.Range(0, selectedPoints.Count);
        selectedPoints[keyIndex].HasKey = true;

        // returning transforms for spawner instantiation
        return selectedPoints.ConvertAll(spawnPoint =>  spawnPoint.transform);
    }

    public Transform GetBossSpawnPoint()
    {
        return bossSpawnPoint.transform;
    }

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Ensure it only triggers once
            roomManager.StartNewRoom(this); // Trigger spawning for this room
        }
    }
}
