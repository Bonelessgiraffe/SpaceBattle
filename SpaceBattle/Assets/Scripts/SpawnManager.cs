using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float[] timeBetweenSpawns;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private GameObject currentEnemy;
   // [SerializeField] private float currentTimeBetweenSpawns = 3;
    public bool gameOver;
    public float newRoundTime = 20f;
    public float spawnFrequency = 2;
    public static SpawnManager instance;

    private float spawnFrequencyTimer = 0;


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
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        spawnFrequencyTimer += Time.deltaTime;

        if (spawnFrequencyTimer > newRoundTime)
        {
            spawnFrequency *= 0.9f;
            newRoundTime += 20f;
        }
        if (gameOver)
        {
            spawnFrequencyTimer = 0;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (gameOver == false)
        {
            //Debug.Log("Starting Spawn Routine");
            Vector3 spawnPos = new Vector3(Random.Range(-8, 8), 9, 0);
            Instantiate(Enemies[Random.Range(0,3)], spawnPos, Quaternion.identity);
            //Debug.Log("Enemy spawned");
            yield return new WaitForSeconds(spawnFrequency);
        }       
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (gameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Vector3 powerUpSpawnPos = new Vector3(Random.Range(-9, 9), 7.4f, 0);
            yield return new WaitForSeconds(Random.Range(4,5));
            Instantiate(powerUps[randomPowerUp], powerUpSpawnPos, Quaternion.identity);
        }
    }
}

