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
        Debug.Log($"{room.name}'s key acquired.  Crazy things now ::: room: {room} idx: {currentRoomIndex} idx + 1: {currentRoomIndex + 1} ");
        if(!roomsWithKeysObtained.Contains(room))
        {
            roomsWithKeysObtained.Add(room);
            //FIXME: I am testing the other message and must deleted it and run the one below instead.  Don't forget!!!
            StartCoroutine(UIManager.instance.RunNotificationText($"Acquired Key for next area!", 3f));

            //FIXME: Delete this line and uncomment the one above for final product
            //NOTE: Maybe do back to back notification so that 2 messages can alert the player about the boss being around and then you not being able to move on until the boss is dead
            //StartCoroutine(UIManager.instance.RunNotificationText($"Boss Spawned look for unique area!  Boss must die for you to move on", 5f));
            if ((currentRoomIndex) % 3 == 0) 
            {
                // Get boss spawn loc
                Transform bossSpawnPoint = room.GetBossSpawnPoint();
                // Spawn the boss
                GameObject spiderQueen = Instantiate(spiderBoss, bossSpawnPoint.position, Quaternion.identity);
                StartCoroutine(UIManager.instance.RunNotificationText($"Boss Spawned look for unique area!  Boss must die for you to move on", 3f));
                Debug.Log($"Spawned the boss! {spiderQueen} at {bossSpawnPoint} for room: {room.name} for {currentRoomIndex}");
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
