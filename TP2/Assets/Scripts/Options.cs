using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

	public Text enemiesText;
	public Slider enemiesSlider;

	void Start () {
		enemiesSlider.value = (float)StaticGameStats.EnemyMax;
	}
	void FixedUpdate () {

		enemiesText.text = "Number of enemies : " + enemiesSlider.value;
		StaticGameStats.EnemyMax = (int)enemiesSlider.value;
	}
}
