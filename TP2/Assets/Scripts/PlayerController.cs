using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float rateOfFire;
	private float timeOfFire;

	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 400.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKey(KeyCode.Space)&&Time.time>timeOfFire)
		{
			CmdFire();
			timeOfFire=Time.time+rateOfFire;
		}
	}
	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 5.0f);        
	}
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}
