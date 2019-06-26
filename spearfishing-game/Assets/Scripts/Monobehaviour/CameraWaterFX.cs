using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraWaterFX : MonoBehaviour {

    public float maxDepth;

    public Color shallowColor;
    public Color deepColor;

    public float shallowDensity;
    public float deepDensity;

    public PostProcessingProfile SurfaceProfile;
    public PostProcessingProfile UnderwaterProfile;
    
    public ParticleSystemRenderer ambientParticles;



    void Start() {
        RenderSettings.fog = true;
    }

    void Update() {
        if (GetDepth() < 0) { // Above water
            RenderSettings.fogDensity = 0;
            GetComponent<PostProcessingBehaviour>().profile = SurfaceProfile;
            ambientParticles.enabled = false;
        }
        else { // Underwater
            Color fogColor = Color.Lerp(shallowColor, deepColor, GetDepth() / maxDepth);
            float fogDensity = Mathf.Lerp(shallowDensity, deepDensity, GetDepth() / maxDepth);
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
            GetComponent<PostProcessingBehaviour>().profile = UnderwaterProfile;
            ambientParticles.enabled = true;
        }
    }

    float GetDepth() {
        return -transform.position.y;
    }

}