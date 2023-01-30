using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9f;

    private float delayTime = 5f;
    private float repeatRate = 10f;

    // Update is called once per frame
    void Start()
    {
        Instantiate(enemyPrefab, RandomSpawnPosition(), Quaternion.identity);
        InvokeRepeating("SpawnPowerUp", delayTime, repeatRate);
    }


    //Function that spawn in random position
    private Vector3 RandomSpawnPosition() {
        float randX = Random.Range(-spawnRange, spawnRange);
        float randZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(randX, 0, randZ);
    }

    private void SpawnPowerUp() {
        Instantiate(powerupPrefab, RandomSpawnPosition(), Quaternion.identity);
    }
}
