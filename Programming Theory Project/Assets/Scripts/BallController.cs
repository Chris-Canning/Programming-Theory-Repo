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
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletVelocity = 20f;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject front;
    [SerializeField] GameObject lens;

    private GameManager gM;
    private AudioSource audioSource;
    private GameObject bullet;
    private Rigidbody ballRb;
    private TextMesh text;
    private int count;
    private float timeRemaining = 0f;
    private float spotTime = 10f;
    private float elapsed;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        ballRb = GetComponent<Rigidbody>();
        text = GetComponentInChildren<TextMesh>();
        text.text = "";
        timeRemaining = 0;
        if (gameObject.CompareTag("Ball"))
        {
            gun.SetActive(false);
            lens.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Ball")) {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                text.text = ((int)timeRemaining).ToString();
            }
            if (timeRemaining <= 0)
            {
                text.text = "";
                gun.SetActive(false);
                lens.SetActive(false);
                gM.CameraOff();
            }

            elapsed += Time.deltaTime;
            if(elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                if (timeRemaining > 0)
                {
                    bullet = Instantiate(bulletPrefab, front.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = bulletVelocity * front.transform.forward;
                }
            }

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
            if(gameObject.CompareTag("Ball"))
            {
                timeRemaining += spotTime;
                gun.SetActive(true);
                lens.SetActive(true);
                gM.CameraOn();
            }
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
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(clip2);
            gM.UpdateScore(1);
        }
    }
}