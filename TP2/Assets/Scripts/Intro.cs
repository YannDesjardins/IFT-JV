using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {


	void FixedUpdate () {
		if (Input.anyKeyDown) {
			Invoke ("ToMain", 0.5f);
		}
	}

	void ToMain(){
		SceneManager.LoadScene("Main");
	}
}
