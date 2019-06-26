using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    public float buoyancy = 0; //-1 is sink, 0 is nothing, 1 is float to surface
    public float surfaceOffset = 0;
    public float surfaceDrag = 0;
    public float surfaceAngularDrag = 0;
    public float underwaterDrag = 3;
    public float underwaterAngularDrag = 3;
    

    void FixedUpdate()
    {
        if (transform.position.y > surfaceOffset) {
            SurfacePhysics();
        }
        else {
            UnderwaterPhysics();
        }
    }


    void UnderwaterPhysics() {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().angularDrag = underwaterAngularDrag;
        GetComponent<Rigidbody>().drag = underwaterDrag;
    }


    void SurfacePhysics() {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().angularDrag = surfaceDrag;
        GetComponent<Rigidbody>().drag = surfaceDrag;
    }

}
