using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float delayPos = 5f;
    private float timerPos = 6f;
    private float spawnRange = 9f;
    public ParticleSystem sismic;

    private void Start()
    {
        InvokeRepeating("ChangePos", delayPos, timerPos);
    }

    private void ChangePos() {
        transform.position = RandomSpawnPosition();
    }

    private Vector3 RandomSpawnPosition()
    {
        float randX = Random.Range(-spawnRange, spawnRange);
        float randZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(randX, 0, randZ);
    }
}
