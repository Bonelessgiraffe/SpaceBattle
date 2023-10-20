using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;

    private float xBoundary = 9;
    private float yBoundary = 3.5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform rCannon, lCannon;
    [SerializeField] private GameObject laser;
    [SerializeField] private float cannonCoolDown;
    [SerializeField] private float timeBetweenCannon = 1f;

    [SerializeField] private int lives;
    public int maxLives;

    private bool hasBeenHitThisFrame = false;
    [SerializeField] private Collider2D col;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Player instance is not null on awake");
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        cannonCoolDown -= Time.deltaTime;
        CalculatePlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLasers();
        }
    }
    void CalculatePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x <= -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
           
        }    
        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);

        }
        if (transform.position.y < -yBoundary)
        {
            transform.position = new Vector3(transform.position.x, -yBoundary, transform.position.z);

        }
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        }
    }

    private void FireLasers()
    {
        if (cannonCoolDown < 0)
        {
            Instantiate(laser, rCannon.position, Quaternion.identity);
            Instantiate(laser, lCannon.position, Quaternion.identity);
            cannonCoolDown = timeBetweenCannon;
        }

    }
    private void TakeDamage()
    {
        lives--;
        UIManager.instance.UpdateLives(lives);

        if (lives <= 0)
        {
            Destroy(this.gameObject);
            GameManager.instance.gameOver = true;
            SpawnManager.instance.gameOver = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit Player");
            TakeDamage();
            Destroy(other.gameObject);
            hasBeenHitThisFrame = true;
            ColliderOff();
        }
    }
    IEnumerator ColliderOffRoutine()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.4f);
        col.enabled = true;
        hasBeenHitThisFrame = false;
    }

    public void ColliderOff()
    {
        StartCoroutine(ColliderOffRoutine());
    }

    private void ShieldActive()
    {

    }
    private void QuickFire()
    {
        timeBetweenCannon /= 3;
        StartCoroutine(QuickFireCoolDownRoutine());
    }

    IEnumerator QuickFireCoolDownRoutine()
    {
        yield return new WaitForSeconds(5);
        timeBetweenCannon *= 3;
    }
}
