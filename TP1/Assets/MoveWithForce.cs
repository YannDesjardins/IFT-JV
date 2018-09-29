using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithForce : MonoBehaviour {

	private Physics physics;
	public Vector3 forceToApply;
	public float durationOfForce;

	// Use this for initialization
	void Start () {
		physics = GetComponent<Physics> ();
		ApplyForce ();
	}
	
	void ApplyForce(){
		physics.AddForce (forceToApply);
		Invoke ("StopForce", durationOfForce);
	}

	void StopForce(){
		physics.RemoveForce (forceToApply);
	}
}
