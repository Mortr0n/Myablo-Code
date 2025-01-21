using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Room> rooms; // Drag all room GameObjects here in the Inspector
    [SerializeField] private GameObject spawnerPrefab;

    private HashSet<Room> roomsWithKeysObtained = new HashSet<Room>();
    //private Room currentRoom;

    public void StartNewRoom(Room newRoom)
    {
        //currentRoom = newRoom;

        // Get 30% of spawn points and instantiate spawners
        List<Transform> spawnPointTransforms = newRoom.GetRandomSpawnPoints(0.5f);
        Debug.Log($"{spawnPointTransforms} trans's");

        int spawnerCount = 0;
        
        foreach (Transform spawnPointTransform in spawnPointTransforms)
        {
            spawnerCount++;
            
            GameObject spawnerObj = Instantiate(spawnerPrefab, spawnPointTransform.position, Quaternion.identity);
            Debug.Log($"Instantiated {spawnerObj.name}");
            // Optionally, if your spawner needs to know about its key status, handle it here
            SpawnPoint spawnPoint = spawnPointTransform.GetComponent<SpawnPoint>();
            spawnerObj.name = $"Spawner_{spawnerCount}";
            if (spawnPoint != null)
            {
                EnemySpawner spawner = spawnerObj.GetComponent<EnemySpawner>(); 

                spawner.HasKey = spawnPoint.HasKey; // Pass the key status to the spawner
                spawner.ParentRoom = newRoom;
                //if (spawnPoint.HasKey)
                //{
                //    Debug.Log($"Spawner {spawnerObj.name} has key!");
                //    spawner.HasKey = true;
                //}
            }
        }

        Debug.Log($"Started room: {newRoom.name} with {spawnPointTransforms.Count} spawners.");
    }

    private void OnEnable()
    {
        EventsManager.instance.onEnemySpawnerDeath.AddListener(HandleSpawnerDeath);
    }

    private void OnDisable()
    {
        EventsManager.instance.onEnemySpawnerDeath.RemoveListener(HandleSpawnerDeath);
    }

    private void HandleSpawnerDeath(EnemySpawner spawner, bool hadKey, Room parentRoom)
    {
        Debug.Log($"Spawner {spawner.name} died in room {parentRoom.name}. HadKey: {hadKey}");

        if (hadKey)
        {
            Debug.Log($"Key for room {parentRoom.name} obtained!");
            MarkKeyAsObtained(parentRoom); // Notify the room manager that the key has been obtained
        }
    }

    private void MarkKeyAsObtained(Room room)
    {
        if(!roomsWithKeysObtained.Contains(room))
        {
            roomsWithKeysObtained.Add(room);
            Debug.Log($"Key obtained for room {room.keyIndex}");
            StartCoroutine(UIManager.instance.RunNotificationText($"Acquired Key for next area!", 3f));
            
        }
    }

    public bool HasKeyForRoom(Room room)
    {
        return roomsWithKeysObtained.Contains(room);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Room room in rooms)
            {
                //if (room.IsPlayerInRoom(other.transform.position))
                //{
                //    StartNewRoom(room);
                //    break;
                //}
            }
        }
    }
}
