using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        maxLives = PlayerController.instance.maxLives;
    }
    public void MinusLife(int lives)
    {
        for (int i = 0; i < maxLives; i++) 
        {

        }
    }
}
