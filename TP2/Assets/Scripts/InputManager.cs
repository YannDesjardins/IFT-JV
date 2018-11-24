using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    private static Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();


	// Use this for initialization
	void Start () {
        controls.Add("ForwardButton", KeyCode.W);
        controls.Add("BackwardButton", KeyCode.S);
        controls.Add("LeftButton", KeyCode.A);
        controls.Add("RightButton", KeyCode.D);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
