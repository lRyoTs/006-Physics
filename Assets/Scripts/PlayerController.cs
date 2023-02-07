using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 30f;
    private float forwardInput;
    private Rigidbody _rigidbody;
    private GameObject focalPoint;

    //GAME VARIABLES
    private bool hasPowerup = false;
    private float powerupForce = 300f;
    public bool isGameOver = false;
    public ParticleSystem powerupParticle;
    public GameObject[] powerupIndicators;

    //Player Scale
    private Vector3 normalScale;
    private float increasedMass = 100f;
    private float normalMass;
    private float normalSpeed;
    private float increasedSpeed = 200f;


    // Start is called before the first frame update
    void Start()
    {
        normalScale = transform.localScale;
        _rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        normalMass = _rigidbody.mass;
        normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        //Follows the camera rotation axis
        _rigidbody.AddForce(focalPoint.transform.forward * speed * forwardInput);
        
        //Check if gameOver
        if (transform.position.y < -5) {
            isGameOver = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup")) {
            StartCoroutine(PowerupCountDown());
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupParticle.Play();
        }

        if (other.gameObject.CompareTag("Giga"))
        {
            StartCoroutine(GigaCountDown());
            Destroy(other.gameObject);
            powerupParticle.Play();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup) {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(awayFromPlayer * powerupForce, ForceMode.Impulse);
        }
    }

    //Coroutine of the active timer of the power up
    private IEnumerator PowerupCountDown() {
        for (int i = 0; i < powerupIndicators.Length; i++) {
            powerupIndicators[i].SetActive(true);
            yield return new WaitForSeconds(2);
            powerupIndicators[i].SetActive(false);
        }
        hasPowerup = false;
        powerupParticle.Stop();
    }

    //Coroutine of the active timer of the giga powerup
    private IEnumerator GigaCountDown()
    {
        transform.localScale = Vector3.one * 3;
        _rigidbody.mass = increasedMass;
        speed = increasedSpeed;

        yield return new WaitForSeconds(10);

        speed = normalSpeed;
        transform.localScale = normalScale;
        _rigidbody.mass = normalMass;
        powerupParticle.Stop();
    }
}
