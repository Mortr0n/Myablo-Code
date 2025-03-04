using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Room> rooms; // Drag all room GameObjects here in the Inspector
    [SerializeField] private int currentRoomIndex = 0;
    [SerializeField] private int additionalBaseSpawnIndex = 2;
    [SerializeField] private List<GameObject> spawnerPrefab;
    [SerializeField] public float spawnerSpawnPercent = .5f;
    [SerializeField] private GameObject spiderBoss;
    

    private HashSet<Room> roomsWithKeysObtained = new HashSet<Room>();
    //private Room currentRoom;

    public bool IsBossKeyObtained(int roomIndex)
    {
        return roomsWithKeysObtained.Contains(rooms[roomIndex]);
    }

    public void StartNewRoom(Room newRoom)
    {
        currentRoomIndex++;
        // Not sure what to do with end yet, but this should catch endgame
        if (currentRoomIndex > rooms.Count)
        {
            Debug.Log("All rooms completed!");
            return;
        }
        
        
        // Get percentage of spawn points and instantiate spawners.  Adjust to increase as necessary
        List<Transform> spawnPointTransforms = newRoom.GetRandomSpawnPoints(spawnerSpawnPercent);
        //Debug.Log($"{spawnPointTransforms} trans's");

        int spawnerCount = 0;
        
        foreach (Transform spawnPointTransform in spawnPointTransforms)
        {
            spawnerCount++;

            // setting number of random spawners to pull from and then making sure I don't somehow exceed the number of spawners
            int topSpawnIndex = currentRoomIndex + additionalBaseSpawnIndex;
            if (topSpawnIndex >= spawnerPrefab.Count)
            {
                topSpawnIndex = spawnerPrefab.Count - 1;
            }

            int spawnIdx = Random.Range(0, topSpawnIndex);


            GameObject spawnerObj = Instantiate(spawnerPrefab[spawnIdx], spawnPointTransform.position, Quaternion.identity);
            //Debug.Log($"Instantiated {spawnerObj.name}");
            // Optionally, if the spawner needs to know about its key status, handle it here
            SpawnPoint spawnPoint = spawnPointTransform.GetComponent<SpawnPoint>();
            spawnerObj.name = $"Spawner_{spawnerCount}";
            if (spawnPoint != null)
            {
                EnemySpawner spawner = spawnerObj.GetComponent<EnemySpawner>(); 

                spawner.HasKey = spawnPoint.HasKey; // Pass the key status to the spawner
                spawner.ParentRoom = newRoom;
            }
        }
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

        if (hadKey)
        {
            MarkKeyAsObtained(parentRoom); // Notify the room manager that the key has been obtained
        }
    }

    private void MarkKeyAsObtained(Room room)
    {
        if(!roomsWithKeysObtained.Contains(room))
        {
            roomsWithKeysObtained.Add(room);
            StartCoroutine(UIManager.instance.RunNotificationText($"Acquired Key for next area!", 3f));
            if((currentRoomIndex+1) % 3 == 0) 
            {
                // Get boss spawn loc
                Transform bossSpawnPoint = room.GetBossSpawnPoint();
                // Spawn the boss
                Instantiate(spiderBoss, bossSpawnPoint.position, Quaternion.identity);
            }
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

            }
        }
    }
}
