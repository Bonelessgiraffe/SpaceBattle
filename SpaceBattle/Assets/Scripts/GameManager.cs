using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalScore;
    [SerializeField] private GameObject highScorePanel;
    //[SerializeField] private GameObject newHighScorePanel;

    public bool gameOver;

    private int savedHighScore;

    [SerializeField] private string pName;
    public int loadedHS;
    public string loadedHSName;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("GameManager not null");
        }
        instance = this;
        totalScore = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            LaunchGameOverPanel();
        }
    }

    public void UpdateScore(int points)
    {
        totalScore += points;
        UIManager.instance.UpdateScoreUI(totalScore);
    }

    private void LaunchGameOverPanel()
    {
        UIManager.instance.finalScoreText.text = "Final Score: " + totalScore;
        UIManager.instance.gameOverPanel.SetActive(true);
        if (totalScore >= savedHighScore)
        {
            UIManager.instance.NewHighScorePanel.SetActive(true);

        }
    }
    public void SavePlayerName()
    {
        pName = UIManager.instance.nameInput.text;
    }
    public void LoadHighScorePanel()
    {
        // UIManager.instance.NewHighScorePanel.SetActive(false);

        SavePlayerName();
        //HighScoreList.instance.AddHighScore(pName, totalScore);      
        HighScoreList.instance.CheckIfCanAddNewHighScore(new HighScoreElement(pName, totalScore));
        highScorePanel.gameObject.SetActive(true);
    }


    [System.Serializable]
    class SaveData
    {
        public string highScoreName;
        public int highScore;
    }

    public void SaveHighScoreEntry()
    {
        SaveData data = new SaveData();
        data.highScoreName = pName;
        data.highScore = totalScore;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
        
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            loadedHS = data.highScore;
            loadedHSName = data.highScoreName;
        }
    }
}
