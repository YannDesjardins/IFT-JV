using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarModifier : MonoBehaviour {

	public GameObject avatarBody;
	public GameObject avatarHead;
	public Material color1;
	public Material color2;
	public Material color3;

	public Slider headSlider;
	public Slider bodySlider;
	public Button applyChange;

	// Use this for initialization
	void Start () {

		avatarBody.transform.localScale = StaticGameStats.AvatarBodyScale;
		avatarHead.transform.localScale = StaticGameStats.AvatarHeadScale;
		bodySlider.value = StaticGameStats.AvatarBodyScale.x;
		headSlider.value = StaticGameStats.AvatarHeadScale.x;

		color1.SetColor("_Color", Color.green);
		color2.SetColor("_Color", Color.green);
		color3.SetColor("_Color", Color.green);

	}

	void FixedUpdate (){

		StaticGameStats.AvatarBodyScale = new Vector3 (bodySlider.value, bodySlider.value, bodySlider.value);
		StaticGameStats.AvatarHeadScale = new Vector3 (headSlider.value, headSlider.value, headSlider.value);
		avatarBody.transform.localScale = StaticGameStats.AvatarBodyScale;
		avatarHead.transform.localScale = StaticGameStats.AvatarHeadScale;

	}

	public void GoBack (){
		SceneManager.LoadScene ("main");
	}

}
