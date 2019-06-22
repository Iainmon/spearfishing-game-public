using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffects : MonoBehaviour {

    public float waterHeight;
    public float maxDepth;

    public Color shallowColor;
    public Color deepColor;

    public float shallowDensity;
    public float deepDensity;

    public PostProcessingProfile SurfaceProfile;
    public PostProcessingProfile UnderwaterProfile;



    void Start() {
        RenderSettings.fog = true;
    }

    void Update() {
        if (GetDepth() <= 0) { // Above water
            RenderSettings.fogDensity = 0;
            GetComponent<PostProcessingBehaviour>().profile = SurfaceProfile;
        }
        else { // Underwater
            Color fogColor = Color.Lerp(shallowColor, deepColor, GetDepth() / maxDepth);
            float fogDensity = Mathf.Lerp(shallowDensity, deepDensity, GetDepth() / maxDepth);
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
            GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile = UnderwaterProfile;
        }
    }

    float GetDepth() {
        return waterHeight - transform.position.y;
    }

}