using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;
    [SerializeField] AudioClip clip4;
    [SerializeField] float ballForce = 200;
    [SerializeField] float ballVelocity = 2;
    [SerializeField] ParticleSystem explosion;

    private GameManager gM;
    private AudioSource audioSource;
    private GameObject bullet;
    private Rigidbody ballRb;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        ballRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Ball")) {
            if (GetComponentInChildren<CameraController>().direction == "StationaryX")
            {
                //ballRb.AddForce(transform.forward * ballForce * Time.deltaTime);
                ballRb.velocity = ballVelocity * transform.forward;
            }
            if (Input.touchCount > 1)
            {
                ballRb.velocity = ballVelocity * transform.forward;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                ballRb.AddForce(transform.forward * ballForce * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                ballRb.AddForce(transform.forward * ballForce * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                ballRb.AddForce(transform.forward * -ballForce * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ballRb.AddForce(transform.right * ballForce * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ballRb.AddForce(transform.right * -ballForce * Time.deltaTime);

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ScoreSpot"))
        {
            collision.gameObject.SetActive(false);
            gM.UpdateScore(5);
            audioSource.PlayOneShot(clip1);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gM.UpdateScore(5);
            audioSource.PlayOneShot(clip2);
        }
        if (collision.gameObject.CompareTag("Rope"))
        {
            audioSource.PlayOneShot(clip3);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            audioSource.PlayOneShot(clip4);
        }
        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Bullet"))
        {
            explosion.Play();
            audioSource.PlayOneShot(clip2);
            gM.UpdateScore(1);
        }
    }
}