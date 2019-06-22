using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterFogEffect : MonoBehaviour {

    public float waterHeight;
    public float maxDepth;

    public Color shallowColor;
    public Color deepColor;

    public float shallowDensity;
    public float deepDensity;


    void Start() {
        RenderSettings.fog = true;
    }

    void Update() {
        if (GetDepth() <= 0) {
            RenderSettings.fogDensity = 0;
        }
        else {
            Color fogColor = Color.Lerp(shallowColor, deepColor, GetDepth() / maxDepth);
            float fogDensity = Mathf.Lerp(shallowDensity, deepDensity, GetDepth() / maxDepth);
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        }
    }

    float GetDepth() {
        return waterHeight - transform.position.y;
    }

}