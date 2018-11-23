using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Source: https://www.youtube.com/playlist?list=PLwyZdDTyvucyAeJ_rbu_fbiUtGOVY55BG

public class PlayerSync : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPosition;
	[SyncVar]
	private Quaternion syncRotation;

	public Transform myTransform;
	private float lerpRate = 15;

	private Vector3 lastPosition;
	private Quaternion lastRotation;
	private float thresholdPosition = 0.5f;
	private float thresholdRotation = 5;


	void FixedUpdate () {
		PositionToClient ();
		LerpPosition ();
		RotationToClient ();
		LerpRotation ();
	}

	void LerpPosition()
	{
		if (!isLocalPlayer) {
			myTransform.position = Vector3.Lerp (myTransform.position, syncPosition, Time.deltaTime * lerpRate);
		}
	}

	void LerpRotation()
	{
		if (!isLocalPlayer) {
			myTransform.rotation = Quaternion.Lerp (myTransform.rotation, syncRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdPositionToServer (Vector3 position)
	{
		syncPosition = position;
	}

	[Command]
	void CmdRotationToServer (Quaternion rotation)
	{
		syncRotation = rotation;
	}

	[ClientCallback]
	void PositionToClient ()
	{
		if (isLocalPlayer) {
			if (Vector3.Distance (myTransform.position, lastPosition) > thresholdPosition) {
			
				CmdPositionToServer (myTransform.position);
				lastPosition = myTransform.position;
			}
		}
	}

	[ClientCallback]
	void RotationToClient ()
	{
		if (isLocalPlayer) {
			if (Quaternion.Angle (myTransform.rotation, lastRotation) > thresholdRotation) {

				CmdRotationToServer (myTransform.rotation);
				lastRotation = myTransform.rotation;
			}
		}
	}
}
