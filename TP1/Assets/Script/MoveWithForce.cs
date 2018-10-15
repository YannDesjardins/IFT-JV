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
        if (physics == null)
        {
            physics = GetComponent<Physics>();
        }
		physics.AddForce (forceToApply);
		Invoke ("StopForce", durationOfForce);
	}

    public void ApplyForce(Vector3 force, float duration)
    {
        forceToApply = force;
        durationOfForce = duration;
        ApplyForce();
    }

	void StopForce(){
		physics.RemoveForce (forceToApply);
	}
}
