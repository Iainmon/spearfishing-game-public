using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject speargun;
    public GameObject speargunTarget;

    [Header("Aiming")]
    public GameObject aimPosition;
    public GameObject restPosition;
    public float aimSpeed;
    public float restPIDFrequency;
    public float aimPIDFrequency;

    [Header("Shooting")]
    public GameObject spearPrefab;
    public GameObject loadedSpear;
    public GameObject spearLoadPosition;
    public float spearSpeed;
    public float recoilForce;
    public float recoilTorque;
    public float reloadTime;
    
    [System.NonSerialized]
    public bool isReloading = false;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            AimGun();
        }
        else {
            RestGun();
        }

        if (loadedSpear != null) {
            if (Input.GetKey(KeyCode.Mouse0)) {
                Shoot();
            }
        }
        else {
            if (Input.GetKey(KeyCode.R) && isReloading == false) {
                StartCoroutine(Reload());
            }
        }
        
    }

    void Shoot() {
        loadedSpear.transform.parent = null;
        loadedSpear.GetComponent<Rigidbody>().isKinematic = false;
        loadedSpear.GetComponent<Rigidbody>().velocity = loadedSpear.transform.forward * spearSpeed + GetComponent<Rigidbody>().velocity;
        loadedSpear.GetComponent<SpearManager>().canStabFish = true;
        loadedSpear = null;
        speargun.GetComponent<Rigidbody>().velocity += -speargun.transform.forward * recoilForce;
        speargun.GetComponent<Rigidbody>().angularVelocity += -speargun.transform.right * recoilTorque;
    }

    IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        loadedSpear = Instantiate(spearPrefab, spearLoadPosition.transform.position, spearLoadPosition.transform.rotation, speargun.transform);
        isReloading = false;
    }


    void AimGun() {
        speargunTarget.transform.position = Vector3.Lerp(speargunTarget.transform.position, aimPosition.transform.position, Time.deltaTime * aimSpeed);
        speargun.GetComponent<RigidbodyPID>().posFrequency = aimPIDFrequency;
    }

    void RestGun() {
        speargunTarget.transform.position = Vector3.Lerp(speargunTarget.transform.position, restPosition.transform.position, Time.deltaTime * aimSpeed);
        speargun.GetComponent<RigidbodyPID>().posFrequency = restPIDFrequency;
    }

}
