using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float[] timeBetweenSpawns;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject currentEnemy;
    [SerializeField] private float currentTimeBetweenSpawns =3;
    public bool gameOver = false;

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
            Vector3 spawnPos = new Vector3(Random.Range(-8, 8), 7, 0);
            Instantiate(currentEnemy, spawnPos, Quaternion.identity);
            //Debug.Log("Enemy spawned");
            yield return new WaitForSeconds(2);
        }
       // StartCoroutine(SpawnRoutine());
    }
}

