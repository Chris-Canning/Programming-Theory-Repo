using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            //gM.audioSource.PlayOneShot(gM.clip4);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("ScoreSpot"))
        {
            collision.gameObject.SetActive(false);
            gM.UpdateScore(5);
            gM.audioSource.PlayOneShot(gM.clip1);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gM.UpdateScore(5);
            gM.audioSource.PlayOneShot(gM.clip2);
        }
        if (collision.gameObject.CompareTag("Rope"))
        {
            gM.audioSource.PlayOneShot(gM.clip3);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            gM.audioSource.PlayOneShot(gM.clip5);
        }
    }
}