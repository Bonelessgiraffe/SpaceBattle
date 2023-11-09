using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    //[SerializeField] private GameObject highScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText, highScoreText2;
    
    public bool isFlashing;
    private float flashInterval = 0.5f;


    public static FlashingText instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Flashing text not null");
        }

        instance = this;
    }
   
  
    public void StartFlashing()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashText());
            return;
        }
    }

    IEnumerator FlashText()
    {
        int flashCount = 9;
        for (int i = 0; i < flashCount; i++)
        {
            
            highScoreText.enabled = !highScoreText.enabled;
            highScoreText2.enabled = !highScoreText2.enabled;
            yield return new WaitForSeconds(flashInterval);
        }

        // Ensure the text is visible after the flashing stops
        highScoreText.enabled = true;
        highScoreText2.enabled = true;
       // isFlashing = false;
        Debug.Log("Flashing finished");
    }
}

