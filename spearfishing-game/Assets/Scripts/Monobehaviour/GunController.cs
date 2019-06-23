using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject speargun;
    public GameObject aimPosition;
    public GameObject restPosition;
    public float aimSpeed;
    

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            AimGun();
        }
        else {
            RestGun();
        }
        
    }


    void AimGun() {
        speargun.transform.position = Vector3.Lerp(speargun.transform.position, aimPosition.transform.position, Time.deltaTime * aimSpeed);
    }

    void RestGun() {
        speargun.transform.position = Vector3.Lerp(speargun.transform.position, restPosition.transform.position, Time.deltaTime * aimSpeed);
    }

}
