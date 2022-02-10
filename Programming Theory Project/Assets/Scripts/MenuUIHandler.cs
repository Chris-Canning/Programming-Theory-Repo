using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField nameField;
    public TMP_Text[] bestScoreText;
    public TMP_Text durationText;
    public Slider durationSlider;
    float sleepTime = 0.2f;
    public AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        //aS = ScoreManager.Instance.GetComponent<AudioSource>();

        if (ScoreManager.Instance.playerName == "")
        {
            startButton.interactable = false;
        }
        else
        {
            nameField.text = ScoreManager.Instance.playerName;
        }
        if(ScoreManager.Instance.Duration != 0)
        {
            durationSlider.value = ScoreManager.Instance.Duration;
        }
        if (ScoreManager.Instance.IsNewScore)
        {
            StartCoroutine(UpdateNewScore());
        }
        else
        {
            Debug.Log("No new update: " + ScoreManager.Instance.Score);
        }

        RefreshScores();

        UpdateDuration();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartNew()
    {
        if (ScoreManager.Instance.playerName != "")
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
        ScoreManager.Instance.SaveBestScore();

#if UNITY_EDITOR         
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void UpdateName()
    {
        Debug.Log("Name updated to: " + nameField.text);
        ScoreManager.Instance.playerName = nameField.text;
        if (ScoreManager.Instance.playerName == "")
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void UpdateDuration()
    {
        ScoreManager.Instance.Duration = (int)durationSlider.value;
        durationText.text = "Duration: " + ScoreManager.Instance.Duration + "(s)";
    }

    void RefreshScores()
    {
        bestScoreText[0].text = "1.";
        bestScoreText[1].text = "2.";
        bestScoreText[2].text = "3.";
        bestScoreText[3].text = "4.";
        bestScoreText[4].text = "5.";
        bestScoreText[5].text = "6.";
        bestScoreText[6].text = "7.";
        bestScoreText[7].text = "8.";
        bestScoreText[8].text = "9.";
        bestScoreText[9].text = "10.";

        StartCoroutine(DelayedRefresh());
    }

    IEnumerator DelayedRefresh()
    {
        yield return new WaitForSeconds(sleepTime);

        if (ScoreManager.Instance.BestScorer[0] != "")
        {
            aS.Play();
            bestScoreText[0].text = "1.  " + ScoreManager.Instance.BestScorer[0] + " : " + ScoreManager.Instance.BestScore[0];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[1] != "")
        {
            aS.Play();
            bestScoreText[1].text = "2.  " + ScoreManager.Instance.BestScorer[1] + " : " + ScoreManager.Instance.BestScore[1];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[2] != "")
        {
            aS.Play();
            bestScoreText[2].text = "3.  " + ScoreManager.Instance.BestScorer[2] + " : " + ScoreManager.Instance.BestScore[2];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[3] != "")
        {
            aS.Play();
            bestScoreText[3].text = "4.  " + ScoreManager.Instance.BestScorer[3] + " : " + ScoreManager.Instance.BestScore[3];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[4] != "")
        {
            aS.Play();
            bestScoreText[4].text = "5.  " + ScoreManager.Instance.BestScorer[4] + " : " + ScoreManager.Instance.BestScore[4];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[5] != "")
        {
            aS.Play();
            bestScoreText[5].text = "6.  " + ScoreManager.Instance.BestScorer[5] + " : " + ScoreManager.Instance.BestScore[5];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[6] != "")
        {
            aS.Play();
            bestScoreText[6].text = "7.  " + ScoreManager.Instance.BestScorer[6] + " : " + ScoreManager.Instance.BestScore[6];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[7] != "")
        {
            aS.Play();
            bestScoreText[7].text = "8.  " + ScoreManager.Instance.BestScorer[7] + " : " + ScoreManager.Instance.BestScore[7];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[8] != "")
        {
            aS.Play();
            bestScoreText[8].text = "9.  " + ScoreManager.Instance.BestScorer[8] + " : " + ScoreManager.Instance.BestScore[8];
            yield return new WaitForSeconds(sleepTime);
        }
        if (ScoreManager.Instance.BestScorer[9] != "")
        {
            aS.Play();
            bestScoreText[9].text = "10. " + ScoreManager.Instance.BestScorer[9] + " : " + ScoreManager.Instance.BestScore[9];
            yield return new WaitForSeconds(sleepTime);
        }
    }

    IEnumerator UpdateNewScore()
    {
        yield return new WaitForSeconds(1);

        if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[9])
        {
            yield return new WaitForSeconds(sleepTime);
            aS.Play();
            ScoreManager.Instance.BestScore[9] = ScoreManager.Instance.Score;
            ScoreManager.Instance.BestScorer[9] = ScoreManager.Instance.playerName;

            if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[8])
            {
                yield return new WaitForSeconds(sleepTime);
                aS.Play();
                ScoreManager.Instance.BestScore[9] = ScoreManager.Instance.BestScore[8];
                ScoreManager.Instance.BestScorer[9] = ScoreManager.Instance.BestScorer[8];
                ScoreManager.Instance.BestScore[8] = ScoreManager.Instance.Score;
                ScoreManager.Instance.BestScorer[8] = ScoreManager.Instance.playerName;

                if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[7])
                {
                    yield return new WaitForSeconds(sleepTime);
                    aS.Play();
                    ScoreManager.Instance.BestScore[8] = ScoreManager.Instance.BestScore[7];
                    ScoreManager.Instance.BestScorer[8] = ScoreManager.Instance.BestScorer[7];
                    ScoreManager.Instance.BestScore[7] = ScoreManager.Instance.Score;
                    ScoreManager.Instance.BestScorer[7] = ScoreManager.Instance.playerName;

                    if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[6])
                    {
                        yield return new WaitForSeconds(sleepTime);
                        aS.Play();
                        ScoreManager.Instance.BestScore[7] = ScoreManager.Instance.BestScore[6];
                        ScoreManager.Instance.BestScorer[7] = ScoreManager.Instance.BestScorer[6];
                        ScoreManager.Instance.BestScore[6] = ScoreManager.Instance.Score;
                        ScoreManager.Instance.BestScorer[6] = ScoreManager.Instance.playerName;

                        if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[5])
                        {
                            yield return new WaitForSeconds(sleepTime);
                            aS.Play();
                            ScoreManager.Instance.BestScore[6] = ScoreManager.Instance.BestScore[5];
                            ScoreManager.Instance.BestScorer[6] = ScoreManager.Instance.BestScorer[5];
                            ScoreManager.Instance.BestScore[5] = ScoreManager.Instance.Score;
                            ScoreManager.Instance.BestScorer[5] = ScoreManager.Instance.playerName;

                            if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[4])
                            {
                                yield return new WaitForSeconds(sleepTime);
                                aS.Play();
                                ScoreManager.Instance.BestScore[5] = ScoreManager.Instance.BestScore[4];
                                ScoreManager.Instance.BestScorer[5] = ScoreManager.Instance.BestScorer[4];
                                ScoreManager.Instance.BestScore[4] = ScoreManager.Instance.Score;
                                ScoreManager.Instance.BestScorer[4] = ScoreManager.Instance.playerName;

                                if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[3])
                                {
                                    yield return new WaitForSeconds(sleepTime);
                                    aS.Play();
                                    ScoreManager.Instance.BestScore[4] = ScoreManager.Instance.BestScore[3];
                                    ScoreManager.Instance.BestScorer[4] = ScoreManager.Instance.BestScorer[3];
                                    ScoreManager.Instance.BestScore[3] = ScoreManager.Instance.Score;
                                    ScoreManager.Instance.BestScorer[3] = ScoreManager.Instance.playerName;

                                    if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[2])
                                    {
                                        yield return new WaitForSeconds(sleepTime);
                                        aS.Play();
                                        ScoreManager.Instance.BestScore[3] = ScoreManager.Instance.BestScore[2];
                                        ScoreManager.Instance.BestScorer[3] = ScoreManager.Instance.BestScorer[2];
                                        ScoreManager.Instance.BestScore[2] = ScoreManager.Instance.Score;
                                        ScoreManager.Instance.BestScorer[2] = ScoreManager.Instance.playerName;

                                        if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[1])
                                        {
                                            yield return new WaitForSeconds(sleepTime);
                                            aS.Play();
                                            ScoreManager.Instance.BestScore[2] = ScoreManager.Instance.BestScore[1];
                                            ScoreManager.Instance.BestScorer[2] = ScoreManager.Instance.BestScorer[1];
                                            ScoreManager.Instance.BestScore[1] = ScoreManager.Instance.Score;
                                            ScoreManager.Instance.BestScorer[1] = ScoreManager.Instance.playerName;

                                            if (ScoreManager.Instance.Score > ScoreManager.Instance.BestScore[0])
                                            {
                                                yield return new WaitForSeconds(sleepTime);
                                                aS.Play();
                                                ScoreManager.Instance.BestScore[1] = ScoreManager.Instance.BestScore[0];
                                                ScoreManager.Instance.BestScorer[1] = ScoreManager.Instance.BestScorer[0];
                                                ScoreManager.Instance.BestScore[0] = ScoreManager.Instance.Score;
                                                ScoreManager.Instance.BestScorer[0] = ScoreManager.Instance.playerName;
                                            }

                                        }

                                    }

                                }

                            }

                        }

                    }

                }

            }

        }

    }
}
