using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterFogEffect : MonoBehaviour {
    public float waterHeight;

    private bool isUnderwater;
    public Color fogColor;
    
    void Update() {
        if ((transform.position.y < waterHeight) != isUnderwater) {
            isUnderwater = transform.position.y < waterHeight;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }
    }

    void SetNormal() {
        RenderSettings.fog = false;
    }

    void SetUnderwater() {
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = 0.05f;
    }
}