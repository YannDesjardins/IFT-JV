﻿using System.Collections;
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
	public GameObject avatarSantaHat;

	private Mesh avatarHeadMesh;

	public Slider headSlider;
	public Slider bodySlider;
	public Toggle santaHatToggle;
	public Toggle santaHatCombinedToggle;
	public Dropdown color1Dropdown;
	public Dropdown color2Dropdown;
	public Dropdown color3Dropdown;

	// Use this for initialization
	void Start () {

		avatarHeadMesh = avatarHead.GetComponent<MeshFilter> ().mesh;

		avatarBody.transform.localScale = StaticGameStats.AvatarBodyScale;
		avatarHead.transform.localScale = StaticGameStats.AvatarHeadScale;
		bodySlider.value = StaticGameStats.AvatarBodyScale.x;
		headSlider.value = StaticGameStats.AvatarHeadScale.x;
		santaHatToggle.isOn = StaticGameStats.AvatarSantaHat;
		santaHatCombinedToggle.isOn = StaticGameStats.AvatarSantaHatCombined;

		color1Dropdown.value = StaticGameStats.AvatarColor1;
		ChangeColor (color1, StaticGameStats.AvatarColor1);

		color2Dropdown.value = StaticGameStats.AvatarColor2;
		ChangeColor (color2, StaticGameStats.AvatarColor2);

		color3Dropdown.value = StaticGameStats.AvatarColor3;
		ChangeColor (color3, StaticGameStats.AvatarColor3);

		color1Dropdown.onValueChanged.AddListener (delegate {
			Color1DropdownValueChanged (color1Dropdown);
		});

		color2Dropdown.onValueChanged.AddListener (delegate {
			Color2DropdownValueChanged (color2Dropdown);
		});

		color3Dropdown.onValueChanged.AddListener (delegate {
			Color3DropdownValueChanged (color3Dropdown);
		});

	}

	void FixedUpdate (){

		StaticGameStats.AvatarBodyScale = new Vector3 (bodySlider.value, bodySlider.value, bodySlider.value);
		StaticGameStats.AvatarHeadScale = new Vector3 (headSlider.value, headSlider.value, headSlider.value);
		avatarBody.transform.localScale = StaticGameStats.AvatarBodyScale;
		avatarHead.transform.localScale = StaticGameStats.AvatarHeadScale;
		avatarSantaHat.active = StaticGameStats.AvatarSantaHat;


	}

	public void GoBack (){
		SceneManager.LoadScene ("main");
	}

	public void AddSantaHat (){
		StaticGameStats.AvatarSantaHat = santaHatToggle.isOn;
	}

	public void AddSantaHatCombined (){
		StaticGameStats.AvatarSantaHatCombined = santaHatCombinedToggle.isOn;

		if (santaHatCombinedToggle.isOn == true) {
			CombineInstance[] combine = new CombineInstance[2];
			combine [0].mesh = avatarHead.GetComponent<MeshFilter>().sharedMesh;
			combine [1].mesh = avatarSantaHat.GetComponent<MeshFilter>().sharedMesh;
			Mesh avatarCombined = new Mesh();
			avatarCombined.CombineMeshes (combine, false, false);
			avatarHead.GetComponent<MeshFilter> ().sharedMesh = avatarCombined;
		} else{
			avatarHead.GetComponent<MeshFilter> ().sharedMesh = avatarHeadMesh;
		}

	}

	void Color1DropdownValueChanged(Dropdown change){
		StaticGameStats.AvatarColor1 = change.value;
		ChangeColor (color1, change.value);
	}

	void Color2DropdownValueChanged(Dropdown change){
		StaticGameStats.AvatarColor2 = change.value;
		ChangeColor (color2, change.value);
	}

	void Color3DropdownValueChanged(Dropdown change){
		StaticGameStats.AvatarColor3 = change.value;
		ChangeColor (color3, change.value);
	}

	void ChangeColor(Material color, int newcolor){
		if (newcolor == 0){
			color.SetColor("_Color", Color.white);
		}
		else if (newcolor == 1){
			color.SetColor("_Color", Color.black);
		}
		else if (newcolor == 2){
			color.SetColor("_Color", Color.green);
		}
		else if (newcolor == 3){
			color.SetColor("_Color", Color.blue);
		}
		else if (newcolor == 4){
			color.SetColor("_Color", Color.yellow);
		}
	}

}
