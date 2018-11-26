using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeripheralMenu : MonoBehaviour {

    public Slider AccuracySlider;
    public Toggle UsingController;

    // Use this for initialization
    void Start () {
        AccuracySlider.value = (float)StaticGameStats.Accuracy;
        UsingController.isOn = StaticGameStats.UsingController;
    }
	
	// Update is called once per frame
	void Update () {
        StaticGameStats.Accuracy = AccuracySlider.value;
        
    }

    public void OnToggle(bool value)
    {
        StaticGameStats.UsingController = !StaticGameStats.UsingController;
    }
}
