﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PauseGame : MonoBehaviour {
	

	private int timeScale = 1;
	private bool pause = false;

    public GameObject musicPlayer;
    public AudioMixerSnapshot casualSnapshot;
    public AudioMixerSnapshot pauseMenuSnapshot;
	public GameObject menuBackground;
	public GameObject pauseMenu;
	public GameObject gobackButton;
    public AudioClip menuEnterSound;
    public AudioClip menuExitSound;
    private AudioSource soundAudioSource;
    private AudioSource pauseMusicSource;
    private Animator animatorBackground;
	private Animator animatorButton;
    private EnemiesSituation enemiesSituation;

	void Start () {
        enemiesSituation = GameObject.FindGameObjectWithTag("EnemyHandler").GetComponent<EnemiesSituation>();
        soundAudioSource = GetComponent<AudioSource>();
        pauseMusicSource = musicPlayer.GetComponents<AudioSource>()[2];
        animatorBackground = menuBackground.GetComponent<Animator> ();
		animatorButton = gobackButton.GetComponent<Animator> ();
	}

	void Update () {
		Time.timeScale = timeScale;

		if (Input.GetKeyDown ("escape")|| Input.GetKeyDown(KeyCode.Joystick1Button9)) {

			if (pause == false) {
				pause = true;
                pauseMusicSource.Play();
				pauseMenu.SetActive (true);

                soundAudioSource.PlayOneShot(menuEnterSound);
                pauseMenuSnapshot.TransitionTo(0f);

                menuAnimation ();
				buttonAnimation ();
				timeScale = 0;
			}
			else if (pause == true) {
                soundAudioSource.PlayOneShot(menuEnterSound);
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
        enemiesSituation.ActiveSnapshot.TransitionTo(0.5f); 
        pauseMenu.SetActive (false);
    }

}
