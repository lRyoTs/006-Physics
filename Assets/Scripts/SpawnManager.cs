using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Enemy Variables
    public GameObject enemyPrefab;
    private int enemiesInScene;
    private int enemiesPerWave = 1;

    //Powerup Variables
    public GameObject [] powerupPrefab; //Array powerup
    private float spawnRange = 9f; //Spawn Range for powerup and enemy prefabs

    //Player
    private PlayerController playerInfo; 

    private void Start()
    {
        playerInfo = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!playerInfo.isGameOver) {

            enemiesInScene = FindObjectsOfType<Enemy>().Length; //Get enemies in scene
            if (enemiesInScene <= 0) { //Once there are no enemy in scene
                SpawnEnemyWave(enemiesPerWave++); //Do a new wave plus one more enemy
            }
        }

    }


    //Function that spawn in random position
    private Vector3 RandomSpawnPosition() {
        float randX = Random.Range(-spawnRange, spawnRange);
        float randZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(randX, 0, randZ);
    }

    //Function that spawn powerups in Random position
    private void SpawnPowerUp() {
        int randIndex = Random.Range(0, powerupPrefab.Length); 
        Instantiate(powerupPrefab[randIndex], RandomSpawnPosition(), Quaternion.identity);
    }

    private void SpawnEnemyWave(int numEnemies) {
        SpawnPowerUp(); //Spawn powerup with wave
        for (int i = 0; i < numEnemies; i++) {
            Instantiate(enemyPrefab, RandomSpawnPosition(), Quaternion.identity);
        }
    }
}
