using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeripheralMenu : MonoBehaviour {

    public Slider AccuracySlider;

    // Use this for initialization
    void Start () {
        AccuracySlider.value = (float)StaticGameStats.Accuracy;
    }
	
	// Update is called once per frame
	void Update () {
        StaticGameStats.Accuracy = AccuracySlider.value;
    }
}
