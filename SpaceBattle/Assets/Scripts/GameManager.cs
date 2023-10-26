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
        if (HighScoreList.instance.highScoreElementList.Count == 5
            )
        {
            if (totalScore >= HighScoreList.instance.highScoreElementList[4].points)
            {
                UIManager.instance.newHighScorePanel.SetActive(true);
            }
        }
        else
        {
          UIManager.instance.newHighScorePanel.SetActive(true);
        }
    }
    public void SavePlayerName()
    {
        pName = UIManager.instance.nameInput.text;
    }
    public void LoadHighScorePanel()
    {              
        HIghScorePanelUI.instance.UpdateUI(HighScoreList.instance.highScoreElementList);
        highScorePanel.gameObject.SetActive(true);
    }
    public void HighScoreContinue()
    {
        SavePlayerName();
        HighScoreList.instance.CheckIfCanAddNewHighScore(new HighScoreElement(pName, totalScore));
        HighScoreList.instance.SaveHighScores();
        StartCoroutine(LoadingHighScoreTableRoutine());
    }
    IEnumerator LoadingHighScoreTableRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        HighScoreList.instance.LoadHighScores();
        LoadHighScorePanel();
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
