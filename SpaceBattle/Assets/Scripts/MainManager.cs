using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private GameObject controlsPanel;

    private void Awake()
    {
        //HIghScorePanelUI.instance.UpdateUI(HighScoreList.instance.highScoreElementList);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        highScorePanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(false);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadHighScores()
    {
       
        highScorePanel.gameObject.SetActive(true);
        
    }

    public void LoadControlsScreen()
    {
        controlsPanel.gameObject.SetActive(true);
    }
    public void CloseControlPanel()
    {
        controlsPanel.gameObject.SetActive(false);
    }
   
}
