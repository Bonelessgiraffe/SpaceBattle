using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 8;
    [SerializeField] private bool isEnemyLaser;
  //  [SerializeField] private AudioSource laserNoise;
   

    private void Start()
    {
       /* if (laserNoise != null)
        {
            laserNoise.Play();
        }
        else
        {
            Debug.Log("NUll af apparently");
        }
       */
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyLaser)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        CheckPosition();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemyLaser)
        {
            if (other.tag == "Enemy")
            {
                Enemy hit = other.GetComponent<Enemy>();
                hit.TakeDamage();
                Destroy(this.gameObject);
            }
        }

        if (isEnemyLaser )
        {
            if ( other.name == "Player")
            {
                PlayerController player = other.GetComponent<PlayerController>();

                player.TakeDamage();
            }
        }
    }


    public void CheckPosition()
    {
        if (transform.position.y > 9 || transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }
}
