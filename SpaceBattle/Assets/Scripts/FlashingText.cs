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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlashingRoutine()
    {
        Debug.Log("Flashing Routine started");
        highScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        highScoreText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FlashingRoutine());
    }
}
