using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearManager : MonoBehaviour
{
    [System.NonSerialized]
    public bool canStabFish = false;

    public static int allowedSpears = 15;

    private static int nextSpearToDestroy = -15;

    private static int lastSpearId = -1;
    private int id;

    void Start() {
        id = lastSpearId + 1;
        lastSpearId++;
        nextSpearToDestroy++;
    }

    void FixedUpdate() {
        if (id == nextSpearToDestroy) {
            Destroy(this.gameObject);
        }
    }
    
}
