using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpearing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Spear") {
            GameObject spear = col.gameObject;
            if (spear.GetComponent<SpearManager>().canStabFish) {
                Destroy(spear.GetComponent<Rigidbody>());
                spear.transform.position = col.GetContact(0).point - spear.transform.forward / 2;
                spear.transform.parent = transform;
            }
        }
    }

}
