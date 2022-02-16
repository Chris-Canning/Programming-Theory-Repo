using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] scoreSpots;

    [SerializeField] TextMeshProUGUI BestScoreText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI CountdownText;
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject Screen;

    [SerializeField] float timeRemaining;

    private bool m_GameOver = false;
    private GameObject ball;
    private GameObject enemy;
    private ScoreManager sM;
    private Camera BallCamera;
    private bool cameraStatus;

    // Start is called before the first frame update
    void Start()
    {
        sM = ScoreManager.Instance;

        sM.ResetScore();

        float rand = Random.Range(-4, 5);
        ball = Instantiate(ballPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        enemy = Instantiate(enemyPrefab, new Vector3(rand, 2, rand), Quaternion.identity);

        BestScoreText.text = "Best Score: " + sM.GetBestScorer() + " : " + sM.GetBestScore();
        CountdownText.text = "Seconds Remaining: " + sM.Duration;
        timeRemaining = sM.Duration;

        cameraStatus = false;
        BallCamera = ball.GetComponentInChildren<Camera>();
        BallCamera.enabled = cameraStatus;
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
            if (timeRemaining <= 0)
            {
                GameOver(); // ABSTRACTION
            }

            if (ball == false)
            {
                float rand = Random.Range(-4, 5);
                ball = Instantiate(ballPrefab, new Vector3(rand, 2, rand), Quaternion.identity);
                UpdateScore(-10);

                BallCamera = ball.GetComponentInChildren<Camera>();
                BallCamera.enabled = cameraStatus;
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

        }
        else // Game Over
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
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
            sM.UpdateScore(score);
            ScoreText.text = " " + sM.PlayerName + "'s Score: " + sM.Score;
        }
    }

    public void GameOver() // ABSTRACTION
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
        if (sM.IsBestScore())
        {
            BestScoreText.text = "Best Score: " + sM.PlayerName + " : " + sM.Score;
        }
        if (sM.IsInBestScore())
        {
            sM.SetIsNewScore(true);
        }
        else
        {
            sM.SetIsNewScore(false);
        }
    }

    public void BackToTitle()
    {
        GameOver();
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void CameraToggle()
    {
        cameraStatus = !cameraStatus;
        BallCamera.enabled = cameraStatus;
        Screen.SetActive(cameraStatus);
    }
}