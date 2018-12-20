using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float rateOfFire;
    public AudioClip gunSound;

	private float timeOfFire;
	public GameObject playerModel;
	private Animator animatorModel;

	private Vector3 currentPosition;
	private Vector3 lastPosition;

    private AudioSource source;

	void Start() {
        source = GetComponent<AudioSource>();
		animatorModel = playerModel.GetComponent<Animator> ();
		currentPosition = transform.position;
		lastPosition = currentPosition;
	}

	void FixedUpdate () {

		currentPosition = transform.position;

		if (lastPosition == currentPosition) {
			bool isRunning = animatorModel.GetBool ("running");

			animatorModel.SetBool ("running", false);
		} else {
			bool isRunning = animatorModel.GetBool ("running");

			animatorModel.SetBool ("running", true);
		}

		lastPosition = currentPosition;



        float xx;
        float zz;
        if (StaticGameStats.UsingController)
        {
            xx = Input.GetAxis("HorizontalLeft") * Time.deltaTime * 10.0f;
            zz = Input.GetAxis("VerticalLeft")  * Time.deltaTime * 10.0f * (-1);

            float xxx = Mathf.Clamp(Input.GetAxis("HorizontalRight") * StaticGameStats.Accuracy * 10.0f, -1, 1) * Time.deltaTime * 10.0f;
            float zzz = Mathf.Clamp(Input.GetAxis("VerticalRight") * StaticGameStats.Accuracy * 10.0f, -1, 1) * Time.deltaTime * 10.0f * (-1);
            if(new Vector3(xxx, 0, zzz).magnitude >0.001)
                transform.rotation = Quaternion.LookRotation(new Vector3(xxx, 0, zzz));
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

        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Joystick1Button7)) && Time.time>timeOfFire)
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

        source.PlayOneShot(gunSound,0.5f);

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
