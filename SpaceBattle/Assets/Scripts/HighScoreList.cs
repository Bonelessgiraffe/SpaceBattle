using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class HighScoreList : MonoBehaviour
{
    [SerializeField] private Transform entryCsontainer;
    [SerializeField]  private Transform entryTemplate;

    [SerializeField] public List<HighScoreElement> highScoreElementList; 
    int maxCount = 5;

    public static HighScoreList instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Highscore list error");
        }
        instance = this;
        LoadHighScores();
    }


    public void AddHighScore(string name, int score)
    {
        Debug.Log("Addhighscore() called");
        highScoreElementList.Add(new HighScoreElement(name, score));
        //highScoreList.Sort((a, b) => score.CompareTo(a.score)
        if (highScoreElementList.Count > 5)
        {
            highScoreElementList.RemoveAt(highScoreElementList.Count - 1);
            
        }
        SaveHighScores();
    }

    public void CheckIfCanAddNewHighScore(HighScoreElement element)
    {
        Debug.LogError("Check High score called " + element.points + element.playerName);
        for ( int i = 0; i < maxCount; i++)
        {
            Debug.Log(i);
            if (i >= highScoreElementList.Count ||element.points > highScoreElementList[i].points)
            {
                Debug.LogError("Can Add");
                highScoreElementList.Insert(i, element);
                //inserts element at i and moves everything down one space
                while (highScoreElementList.Count > maxCount)
                {
                    highScoreElementList.RemoveAt(maxCount);
                }
                SaveHighScores();
                LoadHighScores();
                break; //break skips the rest of for loop
            }
        }   
       
    }
    [System.Serializable]
    class SaveData
    {
        public List<HighScoreElement> sDHighScoreElementList; 

    }
    public void SaveHighScores()
    {
        Debug.Log("SaveHIghScore Called");

        SaveData data = new SaveData();
        data.sDHighScoreElementList = highScoreElementList;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile2.json", json);        
    }


    public void LoadHighScores()
    {
        Debug.Log("LoadHighScores Called");
        
        string path = Application.persistentDataPath + "/savefile2.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            highScoreElementList = data.sDHighScoreElementList;
          //  HIghScorePanelUI.instance.UpdateUI(highScoreElementList);
        }
        else
        {
            Debug.Log("File does not exist!");
            return;
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    
}
