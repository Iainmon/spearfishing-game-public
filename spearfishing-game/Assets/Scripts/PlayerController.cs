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
    public float speedCap;
    private Rigidbody rb;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start() {
        
        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(10.0f, 10.0f, 10.0f);

    }

    void FixedUpdate() {
        
        float movementY = Input.GetAxis("Vertical");
        float movementX = Input.GetAxis("Horizontal");

        if (!(rb.velocity.magnitude > speedCap)) {
            rb.AddForce((transform.forward * movementY * speed) + (transform.right * movementX * speed));
        }
    }

}