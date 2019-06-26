using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    [Header ("Spears")]
    public GameObject speargun;
    public GameObject speargunTarget;
    public GameObject spearModel;

    [Header("Aiming")]
    public GameObject aimPosition;
    public GameObject restPosition;
    public float aimSpeed;
    public float restPIDFrequency;
    public float aimPIDFrequency;

    [Header("Shooting")]
    public GameObject spearPrefab;
    public GameObject spearSpawnPosition;
    [System.NonSerialized]
    public bool isLoaded = true;
    public float spearSpeed;
    public float recoilForce;
    public float recoilTorque;
    public float reloadTime;

    [System.NonSerialized]
    public bool isReloading = false;


    void Start() {

    }

    void FixedUpdate() {
        // Aiming
        if (Input.GetKey(KeyCode.Mouse1)) {
            AimGun();
        }
        else {
            RestGun();
        }

        if (isLoaded) {
            Loaded();
        }
        else { // If Unloaded
            Unloaded();
        }

    }


    void Loaded(){
        spearModel.SetActive(true);
        if (Input.GetKey(KeyCode.Mouse0)) {
            Shoot();
        }

    }


    void Unloaded() {
        spearModel.SetActive(false);
        if (Input.GetKey(KeyCode.R) && isReloading == false) {
            StartCoroutine(Reload());
        }
    }


    void Shoot() {
        GameObject shotSpear = Instantiate(spearPrefab, spearSpawnPosition.transform.position, spearSpawnPosition.transform.rotation);
        shotSpear.GetComponent<Rigidbody>().velocity = shotSpear.transform.forward * spearSpeed + GetComponent<Rigidbody>().velocity;
        shotSpear.GetComponent<SpearManager>().canStabFish = true;
        speargun.GetComponent<Rigidbody>().velocity += -speargun.transform.forward * recoilForce;
        speargun.GetComponent<Rigidbody>().angularVelocity += -speargun.transform.right * recoilTorque;
        isLoaded = false;
    }


    IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isLoaded = true;
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
