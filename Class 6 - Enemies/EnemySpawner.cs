using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : CombatReceiver
{

    [SerializeField] private GameObject EnemyToSpawn;
    protected List<GameObject> spawnedEnemies = new List<GameObject>();
    protected float spawnRateTime = 1f;
    protected float spawnRange = 5f;
    [SerializeField] protected int maxSpawned = 15;
    protected bool spawnEnabled = true;

    [SerializeField] protected Room parentRoom;
    public Room ParentRoom { get => parentRoom;  set { parentRoom = value; } }

    [SerializeField] protected bool hasKey = false;
    public bool HasKey { get => hasKey; set { hasKey = value; Debug.Log($"Spawner {name} HasKey set to to {hasKey}"); } }
    //[SerializeField] protected float serializedMaxHP = 200f;

    //public override float MaxHP
    //{
    //    get => serializedMaxHP;
    //    set => serializedMaxHP = value;
    //}

    protected override void Start()
    {
        base.Start();

        if (HasKey)
        {
            Debug.Log($"{name} spawner has the key!");
        }
        EventsManager.instance.onEnemyDeath.AddListener(HandleSpawnedEnemyDeath);
        StartSpawner();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.collider.name}: {collision.gameObject.name} was the collision");
    }

    protected virtual void HandleSpawnedEnemyDeath(BasicAI enemyAI)
    {

        GameObject enemy = enemyAI.gameObject;
        //Debug.Log($"Contains: {spawnedEnemies.Contains(enemy)} and skeleton is {enemy} and {enemy.name}");
        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
            Debug.Log("Removed enemy from list via event");
        }
    }


    void Update()
    {
        
    }

    protected virtual void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPos();
        //Vector3 offset = GetRandomRingOffset(minDistanceFromSpawner: 2f, maxDistanceFromSpawner: spawnRange);

        //Vector3 spawnPos = transform.position + offset;

        GameObject spawnedEnemy = Instantiate(EnemyToSpawn, transform.position + spawnPos, Quaternion.identity); 
        //Debug.Log($"spawnedEnemy: {spawnedEnemy.name}");
        spawnedEnemies.Add(spawnedEnemy);
    }


    protected virtual Vector3 GetRandomRingOffset(float minDistanceFromSpawner, float maxDistanceFromSpawner)
    {
        // Random angle in [0..2π)
        float angle = Random.Range(0f, 2f * Mathf.PI);

        // Random distance in [min..max)
        float distance = Random.Range(minDistanceFromSpawner, maxDistanceFromSpawner);

        // Compute offset on X/Z plane
        float x = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);

        // Return offset at some default Y
        return new Vector3(x, 1f, z);
    }

    protected virtual Vector3 GetRandomSpawnPos()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);

        return new Vector3 (xPos, 1, zPos);
    }

   protected virtual IEnumerator SpawnTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        //Debug.Log($"SpawnCount {spawnedEnemies.Count} and max {maxSpawned} and enabled {spawnEnabled}");
        if (spawnedEnemies.Count <= maxSpawned && spawnEnabled) 
        {
            SpawnEnemy();
            StartCoroutine(SpawnTimer(spawnRateTime));
        }
        else
        {
            //Debug.Log("spawn timer start");
            StartCoroutine(SpawnTimer(spawnRateTime));
        }
    }

    public virtual void StartSpawner(float waitTimer = -1f) 
    {
        if (waitTimer < 0f)
        {
            waitTimer = spawnRateTime;
        }
        StartCoroutine(SpawnTimer(waitTimer));
    }

    

    public override void Die()
    {
        if (!alive) return;
        alive = false;

        // TODO: Need small smoke cloud animation or something here!
        EventsManager.instance.onEnemySpawnerDeath.Invoke(this, hasKey, parentRoom);  

        Destroy(gameObject);
    }
}
