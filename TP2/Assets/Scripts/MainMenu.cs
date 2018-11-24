using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider loadingSlider;

	void Start (){
	Time.timeScale = 1;
	}

	public void StartGame (){

		StaticGameStats.EnemyCount = StaticGameStats.EnemyMax;
		StartCoroutine (LoadGameAsync ());

	}
		

	IEnumerator LoadGameAsync(){
		AsyncOperation loading = SceneManager.LoadSceneAsync("Game");
		loadingScreen.SetActive (true);

		while (!loading.isDone) {

			float loadingProgress = Mathf.Clamp01 (loading.progress / .9f);
			loadingSlider.value = loadingProgress;
			yield return null;
		}
	}

	public void QuitGame (){
		Application.Quit ();
	}
}
