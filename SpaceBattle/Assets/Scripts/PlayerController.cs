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
    [SerializeField] private Transform rCannon, lCannon, mCannon;
    [SerializeField] private GameObject laser;
    [SerializeField] private float cannonCoolDown;
    [SerializeField] private float timeBetweenCannon = 1f;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject[] impacts;
    private Transform trans;
    private AudioSource laserAudio;

    public bool tripleShotActive;
    public float speedMultiplier = 2;

    private bool isShieldActive = false;
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
        shieldPrefab.SetActive(false);
        trans = GetComponent<Transform>();
        laserAudio = GetComponent<AudioSource>();
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
            laserAudio.Play();
            if (tripleShotActive == true)
            {
                Instantiate(laser, rCannon.position, Quaternion.identity);
                Instantiate(laser, lCannon.position, Quaternion.identity);
                Instantiate(laser, mCannon.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laser, mCannon.position, Quaternion.identity);
            }
            
            cannonCoolDown = timeBetweenCannon;
        }

    }
    public void TakeDamage()
    {
        Instantiate(impacts[Random.Range(0, 2)], transform.position, Quaternion.identity, trans);
        if (isShieldActive == true)
        {
            isShieldActive = false;
            shieldPrefab.SetActive(false);
            return;
        }
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
            if (isShieldActive == true)
            {
                isShieldActive = false;
                shieldPrefab.SetActive(false);
                return;
            }
            Debug.Log("Enemy hit Player");
            TakeDamage();
           // Destroy(other.gameObject);
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

    public void TripleShotActive()
    {
        tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        tripleShotActive = false;

    }
    public void SpeedBoostActive()
    {
        Debug.Log("SpeedBoost Active called");
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        //isSpeedBoostCollected = false;
        speed /= speedMultiplier;
    }

    public void AddLife()
    {

    }
    public void ShieldActive()
    {
        isShieldActive = true;
        shieldPrefab.SetActive(true);
    }
}
