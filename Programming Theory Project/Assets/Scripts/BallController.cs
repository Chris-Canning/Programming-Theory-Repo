using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;
    [SerializeField] AudioClip clip4;

    private GameManager gM;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
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
    }
}