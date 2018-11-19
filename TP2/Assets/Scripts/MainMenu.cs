using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour {
	
	void Start (){
	Time.timeScale = 1;
	}

	public void StartGame (){
		SceneManager.LoadScene("Game");
	}

	public void QuitGame (){
		Application.Quit ();
	}
}
