using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject enemyPrefab;
    public GameObject[] scoreSpots;

    //public Camera camera;

    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CountdownText;
    public GameObject GameOverText;

    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;

    private bool m_GameOver = false;

    private GameObject ball;
    private GameObject enemy;

    public float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.Score = 0;

        float rand = Random.Range(-4, 5);
        ball = Instantiate(ballPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        enemy = Instantiate(enemyPrefab, new Vector3(rand, 2, rand), Quaternion.identity);

        BestScoreText.text = "Best Score: " + ScoreManager.Instance.BestScorer[0] + " : " + ScoreManager.Instance.BestScore[0];
        CountdownText.text = "Seconds Remaining: " + ScoreManager.Instance.Duration;
        timeRemaining = ScoreManager.Instance.Duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_GameOver)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                CountdownText.text = "Seconds Remaining: " + (int) timeRemaining;
            }
            if (ball == false)
            {
                float rand = Random.Range(-4, 5);
                ball = Instantiate(ballPrefab, new Vector3(rand, 2, rand), Quaternion.identity);
                UpdateScore(-10);
            }
            if (enemy == false)
            {
                float rand = Random.Range(-4, 5);
                enemy = Instantiate(enemyPrefab, new Vector3(rand, 2, rand), Quaternion.identity);
                UpdateScore(-10);
            }
            if (!scoreSpots[0].activeSelf &&
                !scoreSpots[1].activeSelf &&
                !scoreSpots[2].activeSelf &&
                !scoreSpots[3].activeSelf &&
                !scoreSpots[4].activeSelf)
            {
                scoreSpots[0].SetActive(true);
                scoreSpots[1].SetActive(true);
                scoreSpots[2].SetActive(true);
                scoreSpots[3].SetActive(true);
                scoreSpots[4].SetActive(true);
            }

            if (ScoreManager.Instance.Score < -50 || timeRemaining <= 0)
            {
                GameOver();
            }
        }
        else // Game Over
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void UpdateScore(int score)
    {
        if (!m_GameOver)
        {
            ScoreManager.Instance.UpdateScore(score);
            ScoreText.text = ScoreManager.Instance.playerName + "'s Score: " + ScoreManager.Instance.Score;
        }
    }

    public void GameOver()
    {
        m_GameOver = true;

        if(ball == true)
        {
            //Destroy(ball);
        }
        if (enemy == true)
        {
            //Destroy(enemy);
        }

        GameOverText.SetActive(true);
        if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[0])
        {
            //ScoreManager.Instance.IsNewMaxScore = true;
            //ScoreManager.Instance.BestScore[0] = ScoreManager.Instance.Score;
            //ScoreManager.Instance.BestScorer[0] = ScoreManager.Instance.playerName;
            BestScoreText.text = "Best Score: " + ScoreManager.Instance.playerName + " : " + ScoreManager.Instance.Score;
        }
        if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[9])
        {
            ScoreManager.Instance.IsNewScore = true;
        }
        else
        {
            ScoreManager.Instance.IsNewScore = false;
        }
    }

    //float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
    //if (mouseScroll != 0f)
    //{
    //    camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z + mouseScroll);
    //}

    public void BackToTitle()
    {
        GameOver();
        SceneManager.LoadScene(0);
    }
}