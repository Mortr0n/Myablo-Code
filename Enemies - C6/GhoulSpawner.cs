using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhoulSpawner : EnemySpawner
{
    [SerializeField] protected List<GameObject> enemiesToSpawn = new List<GameObject> ();


    protected override void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPos();
        //Vector3 offset = GetRandomRingOffset(minDistanceFromSpawner: 2f, maxDistanceFromSpawner: spawnRange);

        //Vector3 spawnPos = transform.position + offset;
        int spawnIdx = Random.Range(0, enemiesToSpawn.Count);

        GameObject spawnedEnemy = Instantiate(enemiesToSpawn[spawnIdx], transform.position + spawnPos, Quaternion.identity);
        //Debug.Log($"spawnedEnemy: {spawnedEnemy.name}");
        spawnedEnemies.Add(spawnedEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
