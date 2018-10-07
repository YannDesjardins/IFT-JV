using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlane : MonoBehaviour {

	Vector3 planeNormal = Vector3.zero;
	Vector3 planePoint = Vector3.zero;

	// Use this for initialization
	void Start () {
		
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		planeNormal = transform.TransformDirection(mesh.normals[0]);
		planePoint = transform.TransformPoint(mesh.vertices[0]);

	}

	public Vector3 getPlaneNormal (){
		return planeNormal;
	}

	public Vector3 getPlanePoint (){
		return planePoint;
	}

		
}
