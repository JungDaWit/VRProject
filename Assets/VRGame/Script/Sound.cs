using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    public static Sound Instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;

    private void Awake()
    {
        
        Sounds();
    }
    private void Sounds()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(volume) * 20);
    }
}
