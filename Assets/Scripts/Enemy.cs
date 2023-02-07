using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 20;
    private Rigidbody _rigidbody;

    private GameObject player;
    private float outBoundY = -10f;
    private bool onGround = true;

    //Player
    private PlayerController playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerInfo = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInfo.isGameOver) {            
            if (onGround)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                _rigidbody.AddForce(direction * speed);
            }

            //if out of game
            if (transform.position.y < 0) {
                onGround = false;
            }

            //if out of bounds
            if (transform.position.y < outBoundY) {
                Destroy(gameObject);//Destroy enemy
            }
        }

    }
}
