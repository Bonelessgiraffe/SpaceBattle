using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 8;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag  == "Enemy")
        {
            Enemy hit = other.GetComponent<Enemy>();
            hit.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
