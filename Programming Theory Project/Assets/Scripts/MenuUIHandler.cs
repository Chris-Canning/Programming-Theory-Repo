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
    [SerializeField] Button startButton;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_Text[] bestScoreNames;
    [SerializeField] TMP_Text[] bestScores;
    [SerializeField] TMP_Text durationText;
    [SerializeField] Slider durationSlider;

    private float sleepTime = 0.2f;
    private AudioSource audioSource;
    private ScoreManager sM;

    // Start is called before the first frame update
    void Start()
    {
        sM = ScoreManager.Instance;

        audioSource = GetComponent<AudioSource>();

        if (sM.PlayerName == "")
        {
            startButton.interactable = false;
        }
        else
        {
            nameField.text = sM.PlayerName;
        }
        if(sM.Duration != 0)
        {
            durationSlider.value = sM.Duration;
        }
        if (sM.IsNewScore)
        {
            sM.UpdateNewScore();
        }
        else
        {
            Debug.Log("No new update: " + sM.Score);
        }

        RefreshScores(); // ABSTRACTION

        UpdateDuration(); // ABSTRACTION
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartNew()
    {
        if (sM.PlayerName != "")
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
        sM.SaveBestScore();

#if UNITY_EDITOR         
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void UpdateName()
    {
        Debug.Log("Name updated to: " + nameField.text);

        sM.SetPlayerName(nameField.text);
        if (sM.PlayerName == "")
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void UpdateDuration() // ABSTRACTION
    {
        sM.SetDuration((int)durationSlider.value);
        durationText.text = "Duration: " + sM.Duration + "(s)";
    }

    void RefreshScores() // ABSTRACTION
    {
        StartCoroutine(DelayedRefresh());
    }

    IEnumerator DelayedRefresh()
    {
        yield return new WaitForSeconds(sleepTime);

        if (sM.GetBestScorerByIndex(0) != "")
        {
            audioSource.Play();
            bestScoreNames[0].text = sM.GetBestScorerByIndex(0);
            bestScores[0].text = "" + sM.GetBestScoreByIndex(0);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(1) != "")
        {
            audioSource.Play();
            bestScoreNames[1].text = sM.GetBestScorerByIndex(1);
            bestScores[1].text = "" + sM.GetBestScoreByIndex(1);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(2) != "")
        {
            audioSource.Play();
            bestScoreNames[2].text = sM.GetBestScorerByIndex(2);
            bestScores[2].text = "" + sM.GetBestScoreByIndex(2);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(3) != "")
        {
            audioSource.Play();
            bestScoreNames[3].text = sM.GetBestScorerByIndex(3);
            bestScores[3].text = "" + sM.GetBestScoreByIndex(3);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(4) != "")
        {
            audioSource.Play();
            bestScoreNames[4].text = sM.GetBestScorerByIndex(4);
            bestScores[4].text = "" + sM.GetBestScoreByIndex(4);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(5) != "")
        {
            audioSource.Play();
            bestScoreNames[5].text = sM.GetBestScorerByIndex(5);
            bestScores[5].text = "" + sM.GetBestScoreByIndex(5);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(6) != "")
        {
            audioSource.Play();
            bestScoreNames[6].text = sM.GetBestScorerByIndex(6);
            bestScores[6].text = "" + sM.GetBestScoreByIndex(6);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(7) != "")
        {
            audioSource.Play();
            bestScoreNames[7].text = sM.GetBestScorerByIndex(7);
            bestScores[7].text = "" + sM.GetBestScoreByIndex(7);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(8) != "")
        {
            audioSource.Play();
            bestScoreNames[8].text = sM.GetBestScorerByIndex(8);
            bestScores[8].text = "" + sM.GetBestScoreByIndex(8);
            yield return new WaitForSeconds(sleepTime);
        }
        if (sM.GetBestScorerByIndex(9) != "")
        {
            audioSource.Play();
            bestScoreNames[9].text = sM.GetBestScorerByIndex(9);
            bestScores[9].text = "" + sM.GetBestScoreByIndex(9);
            yield return new WaitForSeconds(sleepTime);
        }
    }

}
