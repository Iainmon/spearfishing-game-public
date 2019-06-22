using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float forwardSpeedCap;
    public float sideSpeedCap;
    public float rollSpeed;
    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    private float speedCapDifference;
    private Rigidbody rb;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedCapDifference = sideSpeedCap / forwardSpeedCap;
    }

    void Update()
    {
        Look();
    }
    public void Look()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector3(rotation.x, rotation.y) * lookSpeed;
        //transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation *= Quaternion.AngleAxis(-1 * rollSpeed, transform.forward);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.rotation *= Quaternion.AngleAxis(1 * rollSpeed, transform.forward);
        }

        float movementY = Input.GetAxis("Vertical");
        float movementX = Input.GetAxis("Horizontal");

        float forwardMagnitude = transform.forward.magnitude;
        float sideMagnitude = transform.right.magnitude;

        Vector3 forwardVelocity = new Vector3();
        Vector3 sideVelocity = new Vector3();

        if (forwardMagnitude < forwardSpeedCap)
        {
            forwardVelocity = transform.forward * movementY * speed;
        }
        if (sideMagnitude < sideSpeedCap)
        {
            sideVelocity = transform.right * movementX * (speed * speedCapDifference);
        }
        if (forwardVelocity.magnitude > 0 || sideVelocity.magnitude > 0)
        {
            rb.AddForce(forwardVelocity + sideVelocity);
        }

    }

}