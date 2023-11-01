using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HIghScorePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject highScoreUIElementPrefab;
    [SerializeField] private Transform ElementParent;

    [SerializeField] List<GameObject> uiElements = new List<GameObject>();

    public static HIghScorePanelUI instance;
    // Start is called before the first frame update
    private void Awake()
    {
        panel.gameObject.SetActive(false);
        if (instance != null)
        {
            Debug.Log("Highscore UI Panel Script not null");
        }
        instance = this;
        Debug.Log("High score UI instance set");
            
    }
   
  
    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void UpdateUI(List<HighScoreElement> list)
    {
        Debug.Log("Update HIghscore UI Called");
        for (int i = 0; i < list.Count; i++)
        {
            HighScoreElement element = list[i];

            if (element.points > 0)
            {
                if (i >= uiElements.Count)
                {
                    //instantiate new entry
                    GameObject inst = Instantiate(highScoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(ElementParent, false);
                    uiElements.Add(inst);
                }
            }
            //writes names and points
            Text[] texts = uiElements[i].GetComponentsInChildren<Text>();
           // Debug.LogError(i);
            texts[0].text = element.playerName;
            texts[1].text = element.points.ToString();

            
        }
    }
}
