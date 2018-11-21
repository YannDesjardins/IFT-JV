using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

	public GameObject NetworkManagerGameObject;
	public GameObject gameOverText;

	[SyncVar]
	public int syncEnemyCount;

	public Text enemiesLeftText;

	void FixedUpdate () {
		
		if (isServer) {
			syncEnemyCount = StaticGameStats.EnemyCount;
		}

		enemiesLeftText.text = "Enemies left: " + syncEnemyCount;

		if (StaticGameStats.EnemyCount == 0) {
			RpcGameOver ();
			Invoke ("DedicatedServerShutdown", 3f);
		}
	}
		
	private void DedicatedServerShutdown (){
		
		Destroy(NetworkManagerGameObject);
		NetworkManager.Shutdown();
		SceneManager.LoadScene("Main");
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
