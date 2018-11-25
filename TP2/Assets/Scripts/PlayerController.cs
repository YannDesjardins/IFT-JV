using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float rateOfFire;
	private float timeOfFire;

	void FixedUpdate () {
		if (!isLocalPlayer)
		{
			return;
		}

        float xx;
        float zz;
        if (StaticGameStats.UsingController)
        {
            xx = Input.GetAxis("HorizontalLeft") * Time.deltaTime * 10.0f;
            zz = Input.GetAxis("VerticalLeft") * Time.deltaTime * 10.0f*(-1);
            transform.rotation = Quaternion.LookRotation(new Vector3(xx, 0, zz));
        }
        else
        {
            xx = AxisEmulator.H * Time.deltaTime * 10.0f;
            zz = AxisEmulator.V * Time.deltaTime * 10.0f;

            //Changer direction de tire avec raycast
            //Source: https://www.youtube.com/watch?v=lkDGk3TjsIE
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        transform.Translate(xx, 0, 0, Space.World);
		transform.Translate(0, 0, zz, Space.World);

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
