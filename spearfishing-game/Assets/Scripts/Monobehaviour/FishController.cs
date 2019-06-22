using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    //Public variables
    public string fishName;
    public float alertDistance;
    public float swimSpeed;
    public float fleeSpeed;
    
    //Gameobjects
    GameObject player;

    //LSM
    public enum FishStates {Living, Fleeing}
    [System.NonSerialized]
    public FishStates state = FishStates.Living;

    //Fish Motivators
    [System.NonSerialized]
    public float fear = 0;
    public float maxFear = 100;
    [System.NonSerialized]
    public float hunger = 0;
    public float maxHunger = 100;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update() {
        switch (state) {
            case FishStates.Living:
                float rotX = Mathf.PerlinNoise(Time.time / 8, 1000) * 360;
                float rotY = Mathf.PerlinNoise(Time.time / 8, 2000) * 360;
                float rotZ = Mathf.PerlinNoise(Time.time / 8, 3000) * 360;
                transform.eulerAngles = new Vector3(rotX, rotY, rotZ);
                GetComponent<Rigidbody>().velocity = transform.forward * swimSpeed;

                if (IsAlertedBy(player)) {
                    fear = 100;
                    state = FishStates.Fleeing;
                }
                break;
            case FishStates.Fleeing:
                transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
                GetComponent<Rigidbody>().velocity = transform.forward * fleeSpeed;
                break;
        }
    }

    bool IsAlertedBy(GameObject target) {
        return Vector3.Distance(transform.position, player.transform.position) <= alertDistance && CanSee(target);
    }

    bool CanSee(GameObject target) {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, player.transform.position - transform.position, Vector3.Distance(transform.position, player.transform.position));

        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            if (hit.transform.tag == "Terrain") {
                return false;
            }
        }

        return true;
    }
}
