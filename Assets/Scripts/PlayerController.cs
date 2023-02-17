using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 35f;
    private float forwardInput;
    private Rigidbody _rigidbody;
    private GameObject focalPoint;

    //GAME VARIABLES
    private bool hasPowerup = false;
    private float powerupForce = 450f;
    public bool isGameOver = false;
    public ParticleSystem powerupParticle;
    public GameObject[] powerupIndicators;

    //Player powerups Variables
    private float increasedMass = 100f;
    private float increasedSpeed = 200f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start() { 
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        //Follows the camera rotation axis
        _rigidbody.AddForce(focalPoint.transform.forward * speed * forwardInput); //Movement from camera
        
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

        if (other.gameObject.CompareTag("Giga")) {
            StartCoroutine(GigaCountDown(transform.localScale,_rigidbody.mass,speed));
            Destroy(other.gameObject);
            powerupParticle.Play();
        }

        if (other.gameObject.CompareTag("Sismic")) {

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                AwayFromPlayer(go);
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup) {
            AwayFromPlayer(other.gameObject);
        }
    }

    private void AwayFromPlayer(GameObject enemy) {
        Rigidbody enemyRigidbody = enemy.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (enemy.gameObject.transform.position - transform.position).normalized;
        enemyRigidbody.AddForce(awayFromPlayer * powerupForce, ForceMode.Impulse);
    }

    //Coroutine of the active timer of the power up
    private IEnumerator PowerupCountDown() {

        for (int i = 0; i < powerupIndicators.Length; i++)
        {
            powerupIndicators[i].SetActive(true);
            yield return new WaitForSeconds(2);
            powerupIndicators[i].SetActive(false);
        }
        hasPowerup = false;
  
    }

    //Coroutine of the active timer of the giga powerup
    private IEnumerator GigaCountDown(Vector3 originalScale,float originalMass,float originalSpeed)
    {
        transform.localScale = Vector3.one * 3;
        _rigidbody.mass = increasedMass;
        speed = increasedSpeed;

        yield return new WaitForSeconds(10);

        speed = originalSpeed;
        transform.localScale = originalScale;
        _rigidbody.mass = originalMass;
        powerupParticle.Stop();
    }


}
