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
		RayTraceForward ();
		CalculateCollisionLinePlane ();

	}

	void GetPlanePointAndNormal (){
		
		planeNormal = wall.getPlaneNormal();
		planePoint = wall.getPlanePoint();

	}

	void RayTraceForward () {

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
		Vector3 objectSize = GetComponent<Renderer> ().bounds.size/2;
		float objectSizeZ = objectSize.z;

		currentPos.z = currentPos.z + objectSizeZ;
		dotProduct1 = Vector3.Dot ((currentPos - planePoint), planeNormal);
		dotProduct2 = Vector3.Dot (rayDirection, planeNormal);

		length = dotProduct1 / dotProduct2;
		intersection = rayDirection - rayDirection * length;

		intersection = transform.TransformPoint (intersection);
		Debug.Log (intersection);

		Vector3[] planePoints = wall.getPlanePoints ();
			
		bool pointInsidePlane = ContainsPoint (planePoints, intersection);

		float distanceCollision = (currentPos - intersection).magnitude;


		if (distanceCollision < 1f && pointInsidePlane == true) {
			//Debug.Log ("Collision!");

			physics.StopObject ();

			Vector3 tempPos = transform.position;
			tempPos.z = tempPos.z - 0.1f;
			transform.position = tempPos;
		}

	}

	//Verifie si l'axe des x et y de du point d'intersection font partie du quad de collision.
	//source: http://wiki.unity3d.com/index.php/PolyContainsPoint 
	bool ContainsPoint (Vector3[] polyPoints, Vector3 p){ 
		int j = polyPoints.Length-1; 
		bool inside = false; 
		for (int i = 0; i < polyPoints.Length; j = i++) { 
			if ( ((polyPoints[i].y <= p.y && p.y < polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y < polyPoints[i].y)) && 
				(p.x < (polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x)) {
				inside = !inside; 
			}
		} 
		return inside; 
	}
		
}
