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
			
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;

		transform.Translate(x, 0, 0, Space.World);
		transform.Translate(0, 0, z, Space.World);

		//Changer direction de tire avec raycast
		//Source: https://www.youtube.com/watch?v=lkDGk3TjsIE
		Ray cameraRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayLength;

		if(groundPlane.Raycast(cameraRay, out rayLength)){
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			transform.LookAt (new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}
			
		if (Input.GetKey(KeyCode.Mouse0)&&Time.time>timeOfFire)
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
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;

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
