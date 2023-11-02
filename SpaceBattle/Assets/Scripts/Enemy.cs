using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private float cannonCoolDown;
    [SerializeField] private int health;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform cannonTransform;
    
    [SerializeField] private int scoreValue;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject[] impacts;
    private Transform enemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2, 5);
        cannonCoolDown = Random.Range(2, 4);
        enemyTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        transform.Translate(Vector3.down * speed *Time.deltaTime);
        if (transform.position.y < -12)
        {
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage()
    {
        health--;
        if (health > 0)
        {
            Instantiate(impacts[Random.Range(0, 2)], transform.position, Quaternion.identity);
        }
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            speed = 0.5f;
            Destroy(this.gameObject, 2f);
            GameManager.instance.UpdateScore(scoreValue);
            
        }
    }
   
    private void FireLaser()
    {
        Instantiate(laserPrefab, cannonTransform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
