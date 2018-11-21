using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {

	public GameObject NetworkManagerGameObject;
	public GameObject gameOverText;

	void FixedUpdate () {
		if (StaticGameStats.EnemyCount == 0) {
			RpcGameOver ();
		}
	}

	[ClientRpc]
	private void RpcGameOver (){
		
		gameOverText.SetActive (true);

		Invoke ("RpcQuitGameScene", 2f);

	}

	[ClientRpc]
	private void RpcQuitGameScene(){
		
		Destroy(NetworkManagerGameObject);
		NetworkManager.Shutdown();
		SceneManager.LoadScene("Main");
	}

}
