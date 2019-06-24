using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{

    public GameObject flashlight;
    private bool isOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            isOn = !isOn;
        }

        if (isOn) {
            flashlight.GetComponent<Light>().enabled = true;
        }
        else {
            flashlight.GetComponent<Light>().enabled = false;
        }
    }
}
