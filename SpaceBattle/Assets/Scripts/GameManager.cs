using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalScore;
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private GameObject pauseMenu;

    //[SerializeField] private GameObject newHighScorePanel;q

    public bool gameOver;
    public bool highscoreUploaded = false;

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
        pauseMenu.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && highscoreUploaded == false)
        {
            LaunchGameOverPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            ClosePauseMenu();
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
                FlashingText.instance.StartFlashing();
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
        highscoreUploaded = true;
        CloseGameOverPanels();

        SavePlayerName();
        HighScoreList.instance.CheckIfCanAddNewHighScore(new HighScoreElement(pName, totalScore));
        //HighScoreList.instance.SaveHighScores();
        StartCoroutine(LoadingHighScoreTableRoutine());
    }
    IEnumerator LoadingHighScoreTableRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        HighScoreList.instance.LoadHighScores();
        LoadHighScorePanel();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseGameOverPanels()
    {
        Debug.Log("Close panels called");
        UIManager.instance.gameOverPanel.SetActive(false);
        UIManager.instance.newHighScorePanel.SetActive(false);
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
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
