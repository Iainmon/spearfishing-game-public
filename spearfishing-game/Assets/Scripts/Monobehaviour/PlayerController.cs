using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float maxSpeed;
    public float forwardAccel;
    public float backwardAccel;
    public float strafeAccel;
    public float sprintSpeedMultiplier;
    public float sprintAccelMultiplier;
    public float sneakSpeedMultiplier;
    public float sneakAccelMultiplier;

    [Header("Rotation")]
    public float rollAccel;
    public float lookSpeed;

    [Header("Enviroment")]
    public float underwaterDrag;
    public float underwaterAngularDrag;
    
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate() {
        if (transform.position.y <= 0) {
            Move();
            UnderwaterPhysics();
        }
        else {
            SurfacePhysics();
        }

        Rotate();
        CapSpeed();
    }


    void UnderwaterPhysics() {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().angularDrag = underwaterAngularDrag;
        GetComponent<Rigidbody>().drag = underwaterDrag;
    }


    void SurfacePhysics() {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().angularDrag = 0;
        GetComponent<Rigidbody>().drag = 0;
    }


    void Move() {
        Rigidbody rb = GetComponent<Rigidbody>();
        float _speedMultiplier = 1;
        float _accelMultiplier = 1;
        if (Input.GetKey(KeyCode.LeftShift)) {
            _speedMultiplier *= sprintSpeedMultiplier;
            _accelMultiplier *= sprintAccelMultiplier;
        }
        if (Input.GetKey(KeyCode.LeftControl)){
            _speedMultiplier *= sneakSpeedMultiplier;
            _accelMultiplier *= sneakAccelMultiplier;
        }

        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(transform.forward * forwardAccel * Time.fixedDeltaTime * _accelMultiplier / rb.mass);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-transform.forward * backwardAccel * Time.fixedDeltaTime * _accelMultiplier / rb.mass);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(-transform.right * strafeAccel * Time.fixedDeltaTime * _accelMultiplier / rb.mass);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(transform.right * strafeAccel * Time.fixedDeltaTime * _accelMultiplier / rb.mass);
        }
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddForce(transform.up * strafeAccel * Time.fixedDeltaTime * _accelMultiplier / rb.mass);
        }
        if (Input.GetKey(KeyCode.C)) {
            rb.AddForce(-transform.up * strafeAccel * Time.fixedDeltaTime * _accelMultiplier / rb.mass);
        }
    }


    void Rotate() {
        transform.RotateAround(transform.position, transform.up, Input.GetAxis("Mouse X") * lookSpeed * Time.fixedDeltaTime);
        transform.RotateAround(transform.position, transform.right, -Input.GetAxis("Mouse Y") * lookSpeed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Q)) {
            transform.RotateAround(transform.position, transform.forward, rollAccel * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.RotateAround(transform.position, transform.forward, -rollAccel * Time.fixedDeltaTime);
        }
    }

    
    void CapSpeed() {
        Rigidbody rb = GetComponent<Rigidbody>();
        float speedCap = maxSpeed * sprintSpeedMultiplier;
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}