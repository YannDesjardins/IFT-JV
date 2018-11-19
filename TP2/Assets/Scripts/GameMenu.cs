using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameMenu : MonoBehaviour {
	

	public void MainMenu (){
		


		SceneManager.UnloadSceneAsync ("Game");
		NetworkManager.Shutdown();
		NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();
		if (hud != null) {
			hud.showGUI = false;
		}
		SceneManager.LoadScene("Main");
	}
	
	public void QuitGame (){
		Application.Quit ();
	}
}
