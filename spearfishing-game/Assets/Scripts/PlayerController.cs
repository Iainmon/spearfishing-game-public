// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections;

// public class PlayerController : MonoBehaviour
// {

//     public float speed;

//     private Rigidbody rb;
//     private int count;

//     public float oxegenLevel;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         count = 0;
//     }

//     void FixedUpdate()
//     {
//         float moveHorizontal = Input.GetAxis("Horizontal");
//         float moveVertical = Input.GetAxis("Vertical");

//         Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

//         rb.AddForce(movement * speed);
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("Pick Up"))
//         {
//             other.gameObject.SetActive(false);
//             count = count + 1;
//         }
//     }

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float forwardSpeedCap;
    public float sideSpeedCap;
    private float speedCapDifference;
    private Rigidbody rb;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start() {
        rb = GetComponent<Rigidbody>();
        speedCapDifference = sideSpeedCap / forwardSpeedCap;
    }

    void FixedUpdate() {
        
        float movementY = Input.GetAxis("Vertical");
        float movementX = Input.GetAxis("Horizontal");

        float forwardMagnitude = transform.forward.magnitude;
        float sideMagnitude = transform.right.magnitude;

        Vector3 forwardVelocity = new Vector3();
        Vector3 sideVelocity = new Vector3();

        if (forwardMagnitude < forwardSpeedCap) {
            forwardVelocity = transform.forward * movementY * speed;
        }
        if (sideMagnitude < sideSpeedCap) {
            sideVelocity = transform.right * movementX * (speed * speedCapDifference);
        }
        if (forwardVelocity.magnitude > 0 || sideVelocity.magnitude > 0) {
            rb.AddForce(forwardVelocity + sideVelocity);
        }
    }

}