using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Fortement Base sur l'exemple dans les notes de cours, adapte pour deux axes
public class AxisEmulator : MonoBehaviour {

    public float AxisSensitivity = 0.09f;
    public float AxisGravity = 0.09f;
    public float AxisDeadZone = 0.08999f;

    public static float V { get; set; }
    public static float H { get; set; }


    // Use this for initialization
    void Start () {
        V = 0f;
        H = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        AxisSensitivity = StaticGameStats.Accuracy;

        if (Input.GetKey(StaticGameStats.Controls["ForwardButton"]))
        {
            V = Math.Min(V + AxisSensitivity, 1f);
        }
        else if (Input.GetKey(StaticGameStats.Controls["BackwardButton"]))
        {
            V = Math.Max(V - AxisSensitivity, -1f);
        }
        else if (V >= AxisDeadZone)
        {
            V -= AxisGravity;
        }
        else if (V <= -AxisDeadZone)
        {
            V += AxisGravity;
        }
        else if (Math.Abs(V) < AxisDeadZone)
        {
            V = 0;
        }

        if (Input.GetKey(StaticGameStats.Controls["RightButton"]))
        {
            H = Math.Min(H + AxisSensitivity, 1f);
        }
        else if (Input.GetKey(StaticGameStats.Controls["LeftButton"]))
        {
            H = Math.Max(H - AxisSensitivity, -1f);
        }
        else if (H >= AxisDeadZone)
        {
            H -= AxisGravity;
        }
        else if (H <= -AxisDeadZone)
        {
            H += AxisGravity;
        }
        else if (Math.Abs(H) < AxisDeadZone)
        {
           H = 0;
        }
    }
}
