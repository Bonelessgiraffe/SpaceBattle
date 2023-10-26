using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text newHighScoreText;
    public GameObject newHighScorePanel;
    [SerializeField] private TMP_Text enterPlayerNameText;
    public TMP_InputField nameInput;
    [SerializeField] private GameObject life1, life2, life3;
    private int maxLives;

    
    // Start is called before the first frame update

    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("UI Manager unnasigned");

        }
        instance = this;
    }
    void Start()
    {
        gameOverPanel.SetActive(false);
        newHighScorePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        maxLives = PlayerController.instance.maxLives;

    }
    public void UpdateLives(int lives)
    {
        if (lives < 1)
        {
            life1.SetActive(false);
        }
        if (lives ==1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        } 
        if (lives ==2)
        {
            life2.SetActive(true);
            life3.SetActive(false);
        } 
        if (lives == 3)
        {
            life3.SetActive(true);
        }

        for (int i = 0; i < maxLives; i++) 
        {

        }
    }
    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
