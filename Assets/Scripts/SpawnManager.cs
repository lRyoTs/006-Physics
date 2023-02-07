using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Enemy Variables
    public GameObject enemyPrefab;
    public int enemiesInScene;
    public int enemiesPerWave = 0;

    //Powerup Variables
    public GameObject [] powerupPrefab;
    private float spawnRange = 9f;

    //Player
    private PlayerController playerInfo; 

    private void Awake()
    {
        playerInfo = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!playerInfo.isGameOver) {

            enemiesInScene = FindObjectsOfType<Enemy>().Length; //Get enemies in scene
            if (enemiesInScene <= 0) {
                SpawnEnemyWave(enemiesPerWave++); //Call and postincrement
                SpawnPowerUp();
            }
        }

    }


    //Function that spawn in random position
    private Vector3 RandomSpawnPosition() {
        float randX = Random.Range(-spawnRange, spawnRange);
        float randZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(randX, 0, randZ);
    }

    private void SpawnPowerUp() {
        for (int i = 0; i < powerupPrefab.Length; i++) {
            Instantiate(powerupPrefab[i], RandomSpawnPosition(), Quaternion.identity);
        }
    }

    private void SpawnEnemyWave(int numEnemies) {
        for (int i = 0; i < numEnemies; i++) {
            Instantiate(enemyPrefab, RandomSpawnPosition(), Quaternion.identity);
        }
    }
}
