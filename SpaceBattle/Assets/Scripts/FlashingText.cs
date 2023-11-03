using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    [SerializeField] private GameObject highScoreText;

    public static FlashingText instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Flashing text not null");
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashingRoutine());
    }
    public void Update()
    {
        
    }
    // Update is called once per frame


    IEnumerator FlashingRoutine()
    {

        int flashCount = 3; // The number of times you want it to flash.

        for (int i = 0; i < flashCount; i++)
        {
            highScoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            highScoreText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        /*
        Debug.Log("Flashing Routine started");
        highScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        highScoreText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FlashingRoutine());*/
    }
}
