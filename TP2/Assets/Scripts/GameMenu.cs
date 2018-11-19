using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameMenu : MonoBehaviour {
	
	public GameObject NetworkManagerGameObject;

	public void MainMenu (){

		Destroy(NetworkManagerGameObject);
		NetworkManager.Shutdown();
		SceneManager.LoadScene("Main");
	}
	
	public void QuitGame (){
		Application.Quit ();
	}
}
