using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class HighScoreList : MonoBehaviour
{
    [SerializeField] private Transform entryCsontainer;
    [SerializeField]  private Transform entryTemplate;

    [SerializeField] private List<HighScoreElement> highScoreElementList = new List<HighScoreElement>();
    int maxCount = 5;

    public static HighScoreList instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Highscore list error");
        }
        instance = this;
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
    }
    public void SaveHighScores()
    {
        //string jsonData = JsonUtility.ToJson(new HighScoreList{ entries = highScoreList})
        List<HighScoreElement> highScoreElementList = new List<HighScoreElement>();
        string json = JsonUtility.ToJson(highScoreElementList);

        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void LoadHighScores()
    {
        Debug.Log("HI!");
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            HighScoreList newHighScore = JsonUtility.FromJson<HighScoreList>(json);

           
        }
    }

    public void CheckIfCanAddNewHighScore(HighScoreElement highscoreEntry)
    {

 
    
    }


}
