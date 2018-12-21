using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Tire de https://johnleonardfrench.com/articles/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
/// </summary>
public class SliderVolume : MonoBehaviour
{
    public Slider slider;
    public AudioMixer mixer;
    public string mixerType;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerType, Mathf.Log10(sliderValue) * 20);
    }

    public void Start()
    {
        float soundValue;
        mixer.GetFloat(mixerType, out soundValue);
        slider.value = Mathf.Pow(10, soundValue / 20);
    }
}