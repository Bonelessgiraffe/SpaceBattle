using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    public static MainManager instance;

    private void Awake()
    {
        if (instance != null )
        {
            Debug.Log("Main Manager instance fucked ");
        }
        instance = this;
        //HIghScorePanelUI.instance.UpdateUI(HighScoreList.instance.highScoreElementList);
    }
    private void Start()
    {
        highScorePanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        
    }
    public void CloseMainMenuPanel()
    {
        mainMenuPanel.SetActive(false);
    }
    public void OpenMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadHighScores()
    {
       
        highScorePanel.gameObject.SetActive(true);
        CloseMainMenuPanel();
        
    }

    public void LoadControlsScreen()
    {
        controlsPanel.gameObject.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void CloseControlPanel()
    {
        controlsPanel.gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
   
}
