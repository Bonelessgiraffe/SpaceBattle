using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float[] timeBetweenSpawns;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject currentEnemy;
    [SerializeField] private float currentTimeBetweenSpawns =3;
    public bool gameOver;

    public static SpawnManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Spawn Manager already exists");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator SpawnRoutine()
    {
        while (gameOver == false)
        {
            //Debug.Log("Starting Spawn Routine");
            Vector3 spawnPos = new Vector3(Random.Range(-8, 8), 9, 0);
            Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnPos, Quaternion.identity);
            //Debug.Log("Enemy spawned");
            yield return new WaitForSeconds(2);
        }
       
    }
}

