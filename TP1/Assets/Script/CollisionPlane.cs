using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlane : MonoBehaviour {

	Vector3 planeNormal = Vector3.zero;
	Vector3 planePoint = Vector3.zero;
	Vector3[] planePoints = new Vector3 [4];
	Vector3[] verticeFlipper = new Vector3[2];
    Vector3[] planePointsLocal = new Vector3[4];

	// Use this for initialization
	void Start () {
		
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		planeNormal = transform.TransformDirection(mesh.normals[0]);
		planePoint = transform.TransformPoint(mesh.vertices[0]);

		for(int i = 0; i < mesh.vertices.Length; i++) {

			planePoints[i] = transform.TransformPoint(mesh.vertices[i]);
            planePointsLocal[i] = mesh.vertices[i];
		}

		verticeFlipper[0] = planePoints [1];
		verticeFlipper[1] = planePoints [2];
		planePoints [1] = verticeFlipper [1];
		planePoints [2] = verticeFlipper [0];

        verticeFlipper[0] = planePointsLocal[1];
        verticeFlipper[1] = planePointsLocal[2];
        planePointsLocal[1] = verticeFlipper[1];
        planePointsLocal[2] = verticeFlipper[0];
    }

	public Vector3 getPlaneNormal (){
		return planeNormal;
	}

	public Vector3 getPlanePoint (){
		return planePoint;
	}

	public Vector3[] getPlanePoints (){
		return planePoints;
	}

    public Vector3[] getPlanePointsLocal()
    {
        return planePointsLocal;
    }

}
