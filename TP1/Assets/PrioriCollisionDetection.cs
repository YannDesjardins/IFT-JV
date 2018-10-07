using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioriCollisionDetection : MonoBehaviour {
	Vector3 rayLength;
	Vector3 rayDirection;
	Vector3 currentPos;
	Vector3 planeNormal = Vector3.zero;
	Vector3 planePoint = Vector3.zero;
	CollisionPlane wall;
	private Physics physics;


	// Use this for initialization
	void Start () {
		wall = GameObject.Find("Collision").GetComponent(typeof(CollisionPlane)) as CollisionPlane;
		physics = GetComponent<Physics> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		GetPlanePointAndNormal ();
		RayTrace ();
		CalculateCollisionLinePlane ();
	}

	void GetPlanePointAndNormal (){
		
		planeNormal = wall.getPlaneNormal();
		planePoint = wall.getPlanePoint();

	}

	void RayTrace () {

		currentPos = transform.position;
		rayLength = transform.forward * 50 + currentPos;

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, currentPos);
		lineRenderer.SetPosition(1, rayLength);

		rayDirection = (rayLength - currentPos).normalized;
		
	}

	void CalculateCollisionLinePlane ()
	{
		float length;
		float dotProduct1;
		float dotProduct2;
		Vector3 intersection;

		dotProduct1 = Vector3.Dot ((currentPos - planePoint), planeNormal);
		dotProduct2 = Vector3.Dot (rayDirection, planeNormal);

		length = dotProduct1 / dotProduct2;
		intersection = rayDirection - rayDirection * length;

		//problem si objet scaler//
		intersection = transform.TransformPoint (intersection);

		Vector3 objectSize = GetComponent<Renderer> ().bounds.size/2;
		float objectSizeZ = objectSize.z;
		Debug.Log (objectSize.z);
	

		float distanceCollision = (currentPos - intersection).magnitude - objectSizeZ;


		if (distanceCollision < 1.5f) {
			//Debug.Log ("Collision!");

			physics.StopObject ();
		
			Vector3 tempPos = transform.position;
			tempPos.z = tempPos.z - 0.1f;
			transform.position = tempPos;
		}
	}
		
}
