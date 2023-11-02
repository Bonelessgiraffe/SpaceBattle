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

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2, 5);
        cannonCoolDown = Random.Range(2, 4);
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
        if (health <= 0)
        {
            Destroy(this.gameObject);
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
