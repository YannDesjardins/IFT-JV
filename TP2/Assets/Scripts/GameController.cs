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
	public GameObject firework1;
	public GameObject firework2;
	public GameObject firework3;
	bool playFireworksOnce = false;

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


	private void RpcGameOver (){


		if (playFireworksOnce == false) {
			Instantiate (firework1, transform.position, Quaternion.identity);
			Invoke ("Firework2", 0.5f);
			Invoke ("Firework3", 1.0f);
			playFireworksOnce = true;
		}
		gameOverText.SetActive (true);

		Invoke ("RpcQuitGameScene", 2f);

	}


	private void RpcQuitGameScene(){
		
		Destroy(NetworkManagerGameObject);
		NetworkManager.Shutdown();
		SceneManager.LoadScene("Main");
	}

	private void Firework2(){
		Instantiate (firework2, transform.position += new Vector3(5f, 5f, 5f), Quaternion.identity);
	}

	private void Firework3(){
		Instantiate (firework3, transform.position += new Vector3 (-10f, 0f, -10f), Quaternion.identity);
	}
}
