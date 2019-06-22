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
    private Vector3 rotation = Vector3.zero;
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
        rotation.x = Mathf.Clamp(rotation.x, -30f, 30f);
        transform.eulerAngles = new Vector3(rotation.x * lookSpeed, rotation.y * lookSpeed, transform.eulerAngles.z);
    }

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(transform.position, transform.forward, -1 * rollSpeed);
            //transform.rotation *= Quaternion.AngleAxis(-1 * rollSpeed, transform.forward);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(transform.position, transform.forward, 1 * rollSpeed);
            //transform.rotation *= Quaternion.AngleAxis(1 * rollSpeed, transform.forward);
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