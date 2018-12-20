using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


/// <summary>
/// Tire de https://johnleonardfrench.com/articles/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
/// </summary>
public class SliderVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public string MixerType;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(MixerType, Mathf.Log10(sliderValue) * 20);
    }
}