using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {
	
	public Vector3 velocity;
	public Vector3 forceTotal;
	public List<Vector3> forcesToSum = new List<Vector3>();
	private float mass = 0.5f;

	// Use this for initialization
	void Start () {
		Vector3 force1 = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
		Vector3 force2 = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
		Vector3 force3 = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

		forcesToSum.Add(force1);
		forcesToSum.Add(force2);
		forcesToSum.Add(force3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		SumForces ();
		ModifyVelocity ();
		transform.position += velocity * Time.deltaTime;
	}

	void SumForces () {
		forceTotal = Vector3.zero;
	
		foreach (Vector3 force in forcesToSum) {
			forceTotal = forceTotal + force;
		}
	}

	void ModifyVelocity () {
		Vector3 acceleration = forceTotal / mass;
		velocity += acceleration * Time.deltaTime;
	}
}


