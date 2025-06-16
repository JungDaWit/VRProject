using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class HpEffect : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;

    [Range(0f, 1f)] public float targetIntensity = 0f;
    public float lerpSpeed = 2f;

    void Start()
    {
        volume.profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetIntensity, Time.deltaTime * lerpSpeed);
        }
    }

    public void SetIntensity(float value)
    {
        targetIntensity = Mathf.Clamp01(value);
    }
}

