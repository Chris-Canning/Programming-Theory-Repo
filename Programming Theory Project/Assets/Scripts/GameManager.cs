using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject enemyPrefab;
    //public Camera camera;
    public TextMeshProUGUI scoreText;
    public int score;

    private GameObject ball;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(-3, 4);
        ball = Instantiate(ballPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        enemy = Instantiate(enemyPrefab, new Vector3(rand, 2, rand), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == false)
        {
            float rand = Random.Range(-3, 4);
            ball = Instantiate(ballPrefab, new Vector3(rand, 2, rand), Quaternion.identity);
            score -= 10;
            scoreText.text = "Score: " + score;
        }
        if (enemy == false)
        {
            float rand = Random.Range(-3, 4);
            enemy = Instantiate(enemyPrefab, new Vector3(rand, 2, rand), Quaternion.identity);
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