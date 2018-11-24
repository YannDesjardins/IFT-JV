using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

	public Text enemiesText;
	public Slider enemiesSlider;

    public Text difficultyText;
    public Slider difficultySlider;

	void Start () {
		enemiesSlider.value = (float)StaticGameStats.EnemyMax;
        difficultySlider.value = (float)StaticGameStats.Difficulty;
	}
	void FixedUpdate () {

		enemiesText.text = "Number of enemies : " + enemiesSlider.value;
		StaticGameStats.EnemyMax = (int)enemiesSlider.value;
        difficultyText.text = "Difficulty of enemies : " + (difficultySlider.value == 1 ? "dumbest" : "dumbish");
        StaticGameStats.Difficulty = (int)difficultySlider.value;
	}
}
