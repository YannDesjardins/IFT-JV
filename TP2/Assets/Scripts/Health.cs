﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;
	public int healthRegen = 0;
	public float invincibilityTime;
	public bool destroyOnDeath;
    public AudioClip deathSound;
    public GameObject remoteSound;

    [SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

    private AudioSource audioSource;
    private float timeLastHit = 0;
	private float regenTime = 0;
	private NetworkStartPosition[] spawnPoints;
    private EnemiesSituation enemiesSituation;
    private ParticleSystemPool particleSystemPool;

	public GameObject fireFX;

	public GameObject playerModel;
	private Animator animatorModel;
    private GameObject mainCamera;

	void Start ()
	{
        particleSystemPool = GameObject.FindGameObjectWithTag("GameController").GetComponent<ParticleSystemPool>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        enemiesSituation = GameObject.FindGameObjectWithTag("EnemyHandler").GetComponent<EnemiesSituation>();
        animatorModel = playerModel.GetComponent<Animator> ();

		if (isLocalPlayer)
		{
			spawnPoints = FindObjectsOfType<NetworkStartPosition>();
		}
	}

	private void Update()
	{
		timeLastHit += Time.deltaTime;
		Regen();
	}

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}

		if (timeLastHit > invincibilityTime)
		{
			timeLastHit = 0;
			currentHealth -= amount;
			if (currentHealth <= 0 && currentHealth >= -1000)
			{
                GameObject sound = Instantiate(remoteSound, transform.position,transform.rotation);
                Destroy(sound,deathSound.length);
                particleSystemPool.PlayParticleSystem(transform);
                if (destroyOnDeath)
				{
                    enemiesSituation.DecreaseAlertedEnemies();
					StaticGameStats.EnemyCount--;
					Destroy(gameObject);

				}
				else
				{
					currentHealth = -1001;

					bool isDead = animatorModel.GetBool ("dead");
					animatorModel.SetBool ("dead", true);
                    audioSource.Stop();

                    gameObject.GetComponent<Rigidbody>().isKinematic = true;

					gameObject.GetComponent<PlayerController>().enabled = false;
					Invoke ("RpcRespawn", 2.0f);


				}
			}
		}	
	}
	void OnChangeHealth (int health)
	{
		healthBar.sizeDelta = new Vector2(health * 1.5f, healthBar.sizeDelta.y);
	}

	private void Regen()
	{
		regenTime += Time.deltaTime;
		if (regenTime > 1)
		{
			regenTime = 0;
			currentHealth += healthRegen;
			if(currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}
		}
	}

	[ClientRpc]
	void RpcRespawn()
	{
		Vector3 spawnPoint = Vector3.zero;
        
        currentHealth = maxHealth;

		animatorModel.SetBool ("dead", false);

		if (isLocalPlayer)
		{
			spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			transform.position = spawnPoint;

			gameObject.GetComponent<Rigidbody>().isKinematic = false;

			gameObject.GetComponent<PlayerController>().enabled = true;
            mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, transform.position.z);
        }
	}
}
