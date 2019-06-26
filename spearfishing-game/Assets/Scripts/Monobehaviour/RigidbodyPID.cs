using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPID : MonoBehaviour
{

    public GameObject target;

    public float posFrequency;
    public float posDamping;

    public float rotFrequency;
    public float rotDamping;

    void FixedUpdate()
    {
        MatchRotation();
        MatchPosition();
    }


    void MatchRotation() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Quaternion desiredRotation = target.transform.rotation;
        float kp = (6f * rotFrequency) * (6f * rotFrequency) * 0.25f;
        float kd = 4.5f * rotFrequency * rotDamping;
        float dt = Time.fixedDeltaTime;
        float g = 1 / (1 + kd * dt + kp * dt * dt);
        float ksg = kp * g;
        float kdg = (kd + kp * dt) * g;
        Vector3 x;
        float xMag;
        Quaternion q = desiredRotation * Quaternion.Inverse(transform.rotation);
        q.ToAngleAxis(out xMag, out x);
        x.Normalize();
        x *= Mathf.Deg2Rad;
        Vector3 pidv = kp * x * xMag - kd * rigidbody.angularVelocity;
        Quaternion rotInertia2World = rigidbody.inertiaTensorRotation * transform.rotation;
        pidv = Quaternion.Inverse(rotInertia2World) * pidv;
        pidv.Scale(rigidbody.inertiaTensor);
        pidv = rotInertia2World * pidv;
        rigidbody.AddTorque(pidv * Time.fixedDeltaTime * 200);
    }


    void MatchPosition() {
        Vector3 Pdes = target.transform.position;
        Vector3 Vdes = Vector3.zero;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float kp = (6f * posFrequency) * (6f * posFrequency) * 0.25f;
        float kd = 4.5f * posFrequency * posDamping;
        float dt = Time.fixedDeltaTime;
        float g = 1 / (1 + kd * dt + kp * dt * dt);
        float ksg = kp * g;
        float kdg = (kd + kp * dt) * g;
        Vector3 Pt0 = transform.position;
        Vector3 Vt0 = rigidbody.velocity;
        Vector3 F = (Pdes - Pt0) * ksg + (Vdes - Vt0) * kdg;
        rigidbody.AddForce(F * Time.fixedDeltaTime * 200);
    }
}
