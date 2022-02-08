using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    //public Camera camera;
    private GameObject ball;
    public TextMeshProUGUI scoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        ball = Instantiate(ballPrefab, new Vector3(0, 1, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == false)
        {
            ball = Instantiate(ballPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            score -= 10;
            scoreText.text = "Score: " + score;
        }

        //float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        //if (mouseScroll != 0f)
        //{
        //    camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z + mouseScroll);
        //}

    }
}