using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseGame : NetworkBehaviour {
	
	[SyncVar]
	private int timeScale = 1;

	[SyncVar]
	private bool pause = false;


	public GameObject pauseMenu;

	void Update () {
		Time.timeScale = timeScale;

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
			timeScale = 0;
			pauseMenu.SetActive (true);
		}
		else if (pause == true) {
			pause = false;
			timeScale = 1;
			pauseMenu.SetActive (false);
		}    

	}

	private void DedicatedServerPauseGame (){
		if (pause == false) {
			pause = true;
			timeScale = 0;
			pauseMenu.SetActive (true);
		}
		else if (pause == true) {
			pause = false;
			timeScale = 1;
			pauseMenu.SetActive (false);
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
