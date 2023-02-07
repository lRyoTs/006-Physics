using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{   
    private float timerPos = 6f;
    private float spawnRange = 9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        StartCoroutine(ChangePos());
    }

    private IEnumerator ChangePos() {
        yield return new WaitForSeconds(timerPos);
        transform.position = RandomSpawnPosition();
        gameObject.SetActive(true);
    }

    private Vector3 RandomSpawnPosition()
    {
        float randX = Random.Range(-spawnRange, spawnRange);
        float randZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(randX, 0, randZ);
    }
}
