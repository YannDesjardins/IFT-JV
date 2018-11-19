using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	
	bool pause = false;
	public GameObject pauseMenu;

	void Update () {

		if (Input.GetKeyDown ("escape")){
			if (pause == false) {
				pause = true;
				Time.timeScale = 0;
				pauseMenu.SetActive (true);
			}
			else if (pause == true) {
				pause = false;
				Time.timeScale = 1;
				pauseMenu.SetActive (false);
			}    
		}

	}
}
