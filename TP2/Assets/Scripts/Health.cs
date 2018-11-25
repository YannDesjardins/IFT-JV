using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;
    public int healthRegen = 0;
    public float invincibilityTime;
    public bool destroyOnDeath;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

    private float timeLastHit = 0;
    private float regenTime = 0;
	private NetworkStartPosition[] spawnPoints;

	void Start ()
	{
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
            if (currentHealth <= 0)
            {
                if (destroyOnDeath)
                {

                    StaticGameStats.EnemyCount--;
                    Destroy(gameObject);


                }
                else
                {
                    currentHealth = maxHealth;

                    RpcRespawn();
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

		if (isLocalPlayer)
		{
			spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			transform.position = spawnPoint;
		}
	}
}
