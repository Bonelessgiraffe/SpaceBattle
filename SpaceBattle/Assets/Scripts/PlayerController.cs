using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private GameObject sprite;

    private float xBoundary = 9;
    private float yBoundary = 3.5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform rCannon, lCannon, mCannon;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject explosion;

   

    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject[] impacts;
    private Transform trans;

    [SerializeField] private AudioSource laserAudio;
    [SerializeField] private AudioSource damageNoise;
    public AudioSource powerUpSound;

    public bool tripleShotActive;
    public float speedMultiplier = 2;

    private bool isShieldActive = false;
    [SerializeField] private int lives;
    public int maxLives;

    private bool hasBeenHitThisFrame = false;
    [SerializeField] private Collider2D col;


    [SerializeField] private Image laserIcon;
    [SerializeField] private float cannonCoolDown;
    [SerializeField] private float timeBetweenCannonFire = 1f;
    public bool isCooling;
    private bool isDead;

    [SerializeField] private GameObject PlayerDamageL, PlayerDamageR;


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

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            CalculatePlayerMovement();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireLasers();
            }
        }
        cannonCoolDown += Time.deltaTime; 
       // laserIcon.fillAmount = cannonCoolDown; //
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
        // laserIcon.fillAmount = 0;//    
        Debug.Log("FireLaser called");


        if (cannonCoolDown > timeBetweenCannonFire)
        {
           
            laserAudio.Play();
            if (tripleShotActive == true)
            {
                Instantiate(laser, rCannon.position, Quaternion.identity);
                Instantiate(laser, lCannon.position, Quaternion.identity);
                Instantiate(laser, mCannon.position, Quaternion.identity);
                UIManager.instance.isCoolingDown = true;
            }
            else
            {
                Instantiate(laser, mCannon.position, Quaternion.identity);
                UIManager.instance.isCoolingDown = true;
            }
            
            cannonCoolDown = 0;
            UIManager.instance.coolDownTimer = cannonCoolDown;
            
        }
        else
        {
            UIManager.instance.isCoolingDown = false;
        }

    }

    public void StartLaserCoolDown()
    {

    }

    public void TakeDamage()
    {
       
        if (isShieldActive == true)
        {
            isShieldActive = false;
            shieldPrefab.SetActive(false);
            return;
        }
        lives--;
        UIManager.instance.UpdateLives(lives);
        Instantiate(impacts[Random.Range(0, 2)], transform.position, Quaternion.identity, trans);
        damageNoise.Play();
        if (lives <= 0)
        {
            isDead = true;
            Destroy(this.gameObject, 2.5f);
            sprite.gameObject.SetActive(false);
            PlayerDamageR.SetActive(false); 
            PlayerDamageL.SetActive(false);
            Instantiate(explosion, transform.position, Quaternion.identity);
            StartCoroutine(DeathSequence());
           
        }
        if (lives == 2)
        {
            PlayerDamageL.SetActive(true);
        }
        if (lives == 1)
        {
            PlayerDamageR.SetActive(true);
        }
    }

    IEnumerator DeathSequence()
    {
        
        yield return new WaitForSeconds(1f);

     
        GameManager.instance.gameOver = true;
        SpawnManager.instance.gameOver = true;

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
        timeBetweenCannonFire /= 3;
        StartCoroutine(QuickFireCoolDownRoutine());
    }

    IEnumerator QuickFireCoolDownRoutine()
    {
        yield return new WaitForSeconds(5);
        timeBetweenCannonFire *= 3;
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
