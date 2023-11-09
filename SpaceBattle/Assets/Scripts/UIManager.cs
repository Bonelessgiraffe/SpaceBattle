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

    [SerializeField] private Image cannonIcon;
    public float coolDownTime = 2;
    public float coolDownTimer = 2;
    public KeyCode cannon;
    public bool isCoolingDown = false;

    
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
        if (isCoolingDown == true)
        {
           // UpdateCannonCoolDownIcon();
        }

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

  /*  public void UpdateCannonCoolDownIcon()
    {
        Debug.Log("UpdateCannonCoolDownIcon() Called");
        
        coolDownTimer -= Time.deltaTime;

        if (coolDownTimer <= 0f)
        {
            isCoolingDown = false;
            cannonIcon.fillAmount = 1;
        }
        else
        {
            cannonIcon.fillAmount = coolDownTimer / coolDownTime;
        }

        if ( isCoolingDown == false)
        {
            isCoolingDown = true;
            cannonIcon.fillAmount = Mathf.Lerp(0, 1, 1);
           
        }
        if (isCoolingDown)
        {
            cannonIcon.fillAmount -= 1 / coolDown * Time.deltaTime;
            if (cannonIcon.fillAmount <= 0)
            {
                cannonIcon.fillAmount = 0;
                isCoolingDown = false;
            }
        }
    } */
}
