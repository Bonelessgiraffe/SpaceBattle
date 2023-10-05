using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    private float xBoundary = 9;

    [SerializeField] private float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();
    }
    void CalculatePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        Debug.Log(horizontalInput);
        Debug.Log(verticalInput);

        transform.Translate(direction * speed * Time.deltaTime);

    }
}
