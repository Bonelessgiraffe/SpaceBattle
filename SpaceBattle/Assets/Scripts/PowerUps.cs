using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int powerUpID;
    //ID for powerups

    [SerializeField] private AudioClip clip;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            PlayerController player = other.transform.GetComponent<PlayerController>();
            if (player != null)
            {

                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("shield");
                        player.ShieldActive();
                        break;
                    case 3:
                        Debug.Log("Extra Life");
                        player.AddLife();
                        break;
                    default:
                        Debug.Log("default Value");
                        break;

                }
                Destroy(this.gameObject);
            }


        }


    }
}
