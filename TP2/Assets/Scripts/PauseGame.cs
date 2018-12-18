using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGame : MonoBehaviour {
	

	private int timeScale = 1;
	private bool pause = false;

	public GameObject menuBackground;
	public GameObject pauseMenu;
	public GameObject gobackButton;
	private Animator animatorBackground;
	private Animator animatorButton;

	void Start () {
		animatorBackground = menuBackground.GetComponent<Animator> ();
		animatorButton = gobackButton.GetComponent<Animator> ();
	}

	void Update () {
		Time.timeScale = timeScale;

		if (Input.GetKeyDown ("escape")|| Input.GetKeyDown(KeyCode.Joystick1Button9)) {

			if (pause == false) {
				pause = true;
				pauseMenu.SetActive (true);
				menuAnimation ();
				buttonAnimation ();
				timeScale = 0;
			}
			else if (pause == true) {
				pause = false;
				menuAnimation ();
				buttonAnimation ();
				timeScale = 1;
				Invoke ("closeMenu", 0.4f);
			}    
				
		}
	}
	private void buttonAnimation (){
		if (animatorButton != null) {
			bool isScaled = animatorButton.GetBool ("scaleButton");

			animatorButton.SetBool ("scaleButton", !isScaled);
		}
	}

	private void menuAnimation (){
		if (animatorBackground != null) {
			bool isOpen = animatorBackground.GetBool ("open");

			animatorBackground.SetBool ("open", !isOpen);
		}
	}

	private void closeMenu (){
		pauseMenu.SetActive (false);
	}

}
