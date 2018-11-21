using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseGame : NetworkBehaviour {
	
	bool pause = false;

	public GameObject pauseMenu;

	void Update () {

		if (Input.GetKeyDown ("escape")) {

			if (isServer && isClient) {
				RpcPauseGame ();
			} else if (isClient) {
				OpenMenu ();
			} else if (isServer) {
				RpcPauseGame ();
				DedicatedServerPauseGame ();
			}

		}
	}
	[ClientRpc]
	private void RpcPauseGame (){
		if (pause == false) {
			pause = true;
			Time.timeScale = 0;
			pauseMenu.SetActive (pause);
		}
		else if (pause == true) {
			pause = false;
			Time.timeScale = 1;
			pauseMenu.SetActive (pause);
		}    

	}

	private void DedicatedServerPauseGame (){
		if (pause == false) {
			pause = true;
			Time.timeScale = 0;
			pauseMenu.SetActive (pause);
		}
		else if (pause == true) {
			pause = false;
			Time.timeScale = 1;
			pauseMenu.SetActive (pause);
		}    

	}

	private void OpenMenu (){
		if (pauseMenu.activeSelf){
			pauseMenu.SetActive (false);
		}
		else {
			pauseMenu.SetActive (true);
		}
	}
}
