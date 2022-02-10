using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;
    public string playerName;
    public int Score;
    public int Duration;

    public string[] BestScorer = new string[10];
    public int[] BestScore = new int[10];
    public bool IsNewScore;

    public TMP_Text[] bestScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Score = 0;
        LoadBestScore();
    }

    public void UpdateScore(int score)
    {
        Score += score;
    }


    [System.Serializable]
    class SaveData
    {
        public int BestScore1;
        public string BestScorer1;
        public int BestScore2;
        public string BestScorer2;
        public int BestScore3;
        public string BestScorer3;
        public int BestScore4;
        public string BestScorer4;
        public int BestScore5;
        public string BestScorer5;
        public int BestScore6;
        public string BestScorer6;
        public int BestScore7;
        public string BestScorer7;
        public int BestScore8;
        public string BestScorer8;
        public int BestScore9;
        public string BestScorer9;
        public int BestScore10;
        public string BestScorer10;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.BestScore1 = BestScore[0];
        data.BestScorer1 = BestScorer[0];
        data.BestScore2 = BestScore[1];
        data.BestScorer2 = BestScorer[1];
        data.BestScore3 = BestScore[2];
        data.BestScorer3 = BestScorer[2];
        data.BestScore4 = BestScore[3];
        data.BestScorer4 = BestScorer[3];
        data.BestScore5 = BestScore[4];
        data.BestScorer5 = BestScorer[4];
        data.BestScore6 = BestScore[5];
        data.BestScorer6 = BestScorer[5];
        data.BestScore7 = BestScore[6];
        data.BestScorer7 = BestScorer[6];
        data.BestScore8 = BestScore[7];
        data.BestScorer8 = BestScorer[7];
        data.BestScore9 = BestScore[8];
        data.BestScorer9 = BestScorer[8];
        data.BestScore10 = BestScore[9];
        data.BestScorer10 = BestScorer[9];

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Saved to: " + Application.persistentDataPath + " " + BestScorer[0] + " : " + BestScore[0]);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScore[0] = data.BestScore1;
            BestScorer[0] = data.BestScorer1;
            BestScore[1] = data.BestScore2;
            BestScorer[1] = data.BestScorer2;
            BestScore[2] = data.BestScore3;
            BestScorer[2] = data.BestScorer3;
            BestScore[3] = data.BestScore4;
            BestScorer[3] = data.BestScorer4;
            BestScore[4] = data.BestScore5;
            BestScorer[4] = data.BestScorer5;
            BestScore[5] = data.BestScore6;
            BestScorer[5] = data.BestScorer6;
            BestScore[6] = data.BestScore7;
            BestScorer[6] = data.BestScorer7;
            BestScore[7] = data.BestScore8;
            BestScorer[7] = data.BestScorer8;
            BestScore[8] = data.BestScore9;
            BestScorer[8] = data.BestScorer9;
            BestScore[9] = data.BestScore10;
            BestScorer[9] = data.BestScorer10;
            Debug.Log("Loaded from: " + Application.persistentDataPath + " " + BestScore[0] + " : " + BestScorer[0]);
        }
        else
        {
            Debug.Log("No file to load from: " + Application.persistentDataPath + " " + BestScore[0] + " " + BestScorer[0]);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
