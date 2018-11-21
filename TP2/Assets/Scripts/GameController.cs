using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameController : MonoBehaviour {

	public GameObject NetworkManagerGameObject;
	public GameObject gameOverText;

	void FixedUpdate () {
		if (StaticGameStats.EnemyCount == 0) {
			GameOver ();
		}
	}

	private void GameOver (){
		
		gameOverText.SetActive (true);

		Invoke ("QuitGameScene", 2f);

	}

	private void QuitGameScene(){
		
		Destroy(NetworkManagerGameObject);
		NetworkManager.Shutdown();
		SceneManager.LoadScene("Main");
	}

}
