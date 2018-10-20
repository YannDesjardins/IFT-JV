using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSphere : AbstractCollider {
    Vector3 center;
    float radius;
    GameObject[] walls;

    // Use this for initialization
    new void Start () {
        base.Start();
        radius = GetComponent<SphereCollider>().radius;
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        center = transform.position;
        
        for(int i = 0; i < walls.Length; i++)
        {
            if (Intersect(walls[i].transform.GetChild(0).GetComponent<CollisionPlane>()))
            {
                wallCollisionReaction();
                break;
            }
        }
    }

    bool Intersect(CollisionPlane wall)
    {

        float d = Vector3.Dot(wall.getPlanePoints()[0], wall.getPlaneNormal());
        Vector3 n = wall.getPlaneNormal();
        float distance = Mathf.Abs(Vector3.Dot(n, center) + d);
        if (Mathf.Abs(Vector3.Dot(n, center) + d) <= radius)
        {
            Vector3 intersectPoint = center + distance * n / n.magnitude;
            intersectPoint = wall.transform.InverseTransformPoint(intersectPoint);
            if (VerifyIfWithinPolygon(wall.getPlanePointsLocal(), intersectPoint)){
                Debug.Log("COLLISION AT POINT: " + wall.transform.TransformPoint(intersectPoint));
                CalculateRebound(n);
                return true;
            }
            
        }
        return false;
    }

    
}
