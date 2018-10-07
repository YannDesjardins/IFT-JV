using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

	public Vector3 velocity;
	public Vector3 forceTotal;
	public List<Vector3> forcesToSum = new List<Vector3>();
	private float mass = 5f;
	private Vector3 acceleration;
	private Vector3 gravity = new Vector3(0.0f, -9.81f, 0.0f);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {

		//Algo Cinematique
		SumForces ();
		ModifyVelocity ();
		transform.position += velocity * Time.deltaTime + ( (0.5f * acceleration) * (Mathf.Pow(Time.deltaTime, 2.0f)));
	}

	void SumForces () {
		forceTotal = Vector3.zero;
	
		foreach (Vector3 force in forcesToSum) {
			forceTotal = forceTotal + force;
		}
	}

	void ModifyVelocity () {
		acceleration = forceTotal / mass;
		velocity += (acceleration + gravity) * Time.deltaTime;
	}
		
	public void AddForce (Vector3 force) {
		forcesToSum.Add (force);
	}

	public void RemoveForce (Vector3 force) {
		forcesToSum.Remove (force);
	}

	public void StopObject () {
		forcesToSum.Clear ();
		acceleration = Vector3.zero;
		velocity = Vector3.zero;
	}
}


