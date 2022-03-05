using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;
    [SerializeField] AudioClip clip4;
    [SerializeField] AudioClip clip5;
    [SerializeField] AudioClip clip6;
    [SerializeField] float ballVelocity = 2;
    [SerializeField] float ballForce = 100;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletVelocity = 20f;
    [SerializeField] GameObject front;
    [SerializeField] float mouseSensitivity = 200f;
    [SerializeField] float upForce = 2000;

    private GameManager gM;
    private AudioSource audioSource;
    private GameObject bullet;
    private Rigidbody ballRb;
    private TextMesh text;
    private float timeRemaining = 0f;
    private float spotTime = 15f;
    private float elapsed;
    private bool lensAnimUp;
    private Animator ballAnimator;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        ballRb = GetComponent<Rigidbody>();
        text = GetComponentInChildren<TextMesh>();
        ballAnimator = GetComponent<Animator>();

        text.text = "";
        timeRemaining = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float y = 0;
        float x = 0;

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Ball")) {

            if (transform.position.y >= 0)
            {
                Vector3 origin = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(origin.x, 6f + transform.position.y, -15f - transform.position.y);
            }

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                text.text = ((int)timeRemaining).ToString();
            }
            if (timeRemaining <= 0)
            {
                text.text = "";
                LensDown();
            }

            elapsed += Time.deltaTime;
            if(elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                if (timeRemaining > 0)
                {
                    if (lensAnimUp)
                    {
                        explosion.Play();
                        audioSource.PlayOneShot(clip5, 0.5f);
                        ballAnimator.SetTrigger("Fire");
                        bullet = Instantiate(bulletPrefab, front.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
                        bullet.GetComponent<Rigidbody>().velocity = bulletVelocity * front.transform.forward;
                    }
                }
            }

            y = getYInput1();
            x = getXInput1();

            //if (Input.touchCount > 1)
            //{
            //    ballRb.velocity = ballVelocity * transform.forward;
            //}

            if (Input.GetKeyDown(KeyCode.F))
            {
                //LensUp();
                //timeRemaining += spotTime;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                //timeRemaining = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ballRb.AddForce(Vector3.up * upForce * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.V) || Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                ballRb.velocity = -ballVelocity * transform.forward;
            }
            if (Input.GetKey(KeyCode.B) || Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                ballRb.velocity = ballVelocity * transform.forward;
            }
            if (Input.GetKey(KeyCode.UpArrow) || y > 0)
            {
                ballRb.AddForce(Vector3.forward * ballForce * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow) || y < 0)
            {
                ballRb.AddForce(Vector3.forward * -ballForce * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow) || x < 0)
            {
                ballRb.AddForce(Vector3.right * ballForce * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || x > 0)
            {
                ballRb.AddForce(Vector3.right * -ballForce * Time.deltaTime);
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
                LensUp();
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //gM.UpdateScore(5);
            audioSource.PlayOneShot(clip2);
        }
        if (collision.gameObject.CompareTag("Rope"))
        {
            audioSource.PlayOneShot(clip2,0.3f);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            audioSource.PlayOneShot(clip4,0.5f);
        }
        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Bullet"))
        {
            explosion.Play();
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(clip6,0.5f);
            gM.UpdateScore(1);
        }
    }

    void LensUp()
    {
        gM.CameraOn();
        ballAnimator.SetBool("LensUp", true);
    }

    void LensDown()
    {
        gM.CameraOff();
        ballAnimator.SetBool("LensUp", false);
    }

    void LensNowUp()
    {
        lensAnimUp = true;
    }

    void LensNowDown()
    {
        lensAnimUp = false;
    }

    // Mouse Input Y
    public float getYInput1()
    {
        float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;

        return mouseVertical;
    }

    // Mouse Input X
    public float getXInput1()
    {
        float mouseHorizontal = -Input.GetAxis("Mouse X") * mouseSensitivity;

        return mouseHorizontal;
    }

}