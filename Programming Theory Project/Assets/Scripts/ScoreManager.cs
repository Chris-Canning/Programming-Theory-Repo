using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance { get; private set; } // ENCAPSULATION

    public string PlayerName { get; private set; } // ENCAPSULATION
    public int Score { get; private set; } // ENCAPSULATION
    public int Duration { get; private set; } // ENCAPSULATION
    public bool IsNewScore { get; private set; } // ENCAPSULATION

    [SerializeField] string[] BestScorer = new string[10];
    [SerializeField] int[] BestScore = new int[10];

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
        LoadBestScore(); // ABSTRACTION
    }

    public void ResetScore() // ENCAPSULATION
    {
        Score = 0;
    }

    public void UpdateScore(int score) // ENCAPSULATION
    {
        Score += score;
    }

    public int GetBestScore()
    {
        return BestScore[0];
    }

    public string GetBestScorer()
    {
        return BestScorer[0];
    }

    public string GetBestScorerByIndex(int theIndex) // ENCAPSULATION
    {
        if(theIndex < 0 || theIndex > BestScorer.Length)
        {
            return "";
        }
        return BestScorer[theIndex];
    }

    public int GetBestScoreByIndex(int theIndex) // ENCAPSULATION
    {
        if (theIndex < 0 || theIndex > BestScore.Length)
        {
            return 0;
        }
        return BestScore[theIndex];
    }

    public void SetPlayerName(string name) // ENCAPSULATION
    {
        PlayerName = name;
    }

    public void SetDuration(int value) // ENCAPSULATION
    {
        Duration = value;
    }

    public bool IsBestScore()
    {
        if (Score > BestScore[0])
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void SetIsNewScore(bool isNewScore)
    {
        IsNewScore = isNewScore;
    }

    public bool IsInBestScore()
    {
        if (Score > BestScore[9])
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public void LoadBestScore() // ABSTRACTION
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
            BestScore[0] = 55;
            BestScorer[0] = "Vi";
            BestScore[1] = 30;
            BestScorer[1] = "Vi";
            BestScore[2] = 30;
            BestScorer[2] = "Milo";
            BestScore[3] = 25;
            BestScorer[3] = "Clagger";
            BestScore[4] = 20;
            BestScorer[4] = "Deckard";
            BestScore[5] = 20;
            BestScorer[5] = "Powder";
            BestScore[6] = 15;
            BestScorer[6] = "Vi";
            BestScore[7] = 10;
            BestScorer[7] = "Milo";
            BestScore[8] = 10;
            BestScorer[8] = "Clagger";
            BestScore[9] = 5;
            BestScorer[9] = "Powder";

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

    public void UpdateNewScore()
    {
        if (Score > BestScore[9])
        {
            BestScore[9] = Score;
            BestScorer[9] = PlayerName;

            if (Score > BestScore[8])
            {
                BestScore[9] = BestScore[8];
                BestScorer[9] = BestScorer[8];
                BestScore[8] = Score;
                BestScorer[8] = PlayerName;

                if (Score > BestScore[7])
                {
                    BestScore[8] = BestScore[7];
                    BestScorer[8] = BestScorer[7];
                    BestScore[7] = Score;
                    BestScorer[7] = PlayerName;

                    if (Score > BestScore[6])
                    {
                        BestScore[7] = BestScore[6];
                        BestScorer[7] = BestScorer[6];
                        BestScore[6] = Score;
                        BestScorer[6] = PlayerName;

                        if (Score > BestScore[5])
                        {
                            BestScore[6] = BestScore[5];
                            BestScorer[6] = BestScorer[5];
                            BestScore[5] = Score;
                            BestScorer[5] = PlayerName;

                            if (Score > BestScore[4])
                            {
                                BestScore[5] = BestScore[4];
                                BestScorer[5] = BestScorer[4];
                                BestScore[4] = Score;
                                BestScorer[4] = PlayerName;

                                if (Score > BestScore[3])
                                {
                                    BestScore[4] = BestScore[3];
                                    BestScorer[4] = BestScorer[3];
                                    BestScore[3] = Score;
                                    BestScorer[3] = PlayerName;

                                    if (Score > BestScore[2])
                                    {
                                        BestScore[3] = BestScore[2];
                                        BestScorer[3] = BestScorer[2];
                                        BestScore[2] = Score;
                                        BestScorer[2] = PlayerName;

                                        if (Score > BestScore[1])
                                        {
                                            BestScore[2] = BestScore[1];
                                            BestScorer[2] = BestScorer[1];
                                            BestScore[1] = Score;
                                            BestScorer[1] = PlayerName;

                                            if (Score > BestScore[0])
                                            {
                                                BestScore[1] = BestScore[0];
                                                BestScorer[1] = BestScorer[0];
                                                BestScore[0] = Score;
                                                BestScorer[0] = PlayerName;
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
