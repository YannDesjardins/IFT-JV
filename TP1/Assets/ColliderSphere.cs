using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSphere : MonoBehaviour {
    Vector3 center;
    float radius;

    Physics physics;

    public float reboundFactor = 1;


	// Use this for initialization
	void Start () {
        
        radius = GetComponent<SphereCollider>().radius;
        physics = GetComponent<Physics>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        center = transform.position;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        for(int i = 0; i < walls.Length; i++)
        {
            Intersect(walls[i].transform.GetChild(0).GetComponent<CollisionPlane>());
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
                Vector3 vel = new Vector3(physics.getVelocity().x, physics.getVelocity().y, physics.getVelocity().z);
                physics.StopObject();
                vel = vel - reboundFactor * (2 * (Vector3.Dot(n, vel) * n));
                physics.setVelocity(vel);
                return true;
            }
            
        }
        return false;
    }

    bool VerifyIfWithinPolygon(Vector3[] vertices, Vector3 point)
    {
        Vector3 pointProjection = new Vector3(point.x + 100, point.y + 100, point.z);
        Vector3 ray = pointProjection - point;
        int j = vertices.Length - 1;
        int intersection = 0;
        for (int i = 0; i < vertices.Length; j = i++)
        {
            if (IntersectionVecteur2d(point, ray, vertices[j], vertices[i] - vertices[j]))
            {
                intersection++;
            }
        }
        return intersection % 2 == 1;
    }

    bool IntersectionVecteur2d(Vector3 p, Vector3 v, Vector3 q, Vector3 w)
    {
        float alpha = ((p.y - q.y) * w.x - (p.x - q.x) * w.y) / (v.x * w.y - v.y * w.x);
        float beta;
        if (w.x != 0)
        {
            beta = (p.x + v.x * alpha - q.x) / w.x;
        }
        else
        {
            beta = (p.y + v.y * alpha - q.y) / w.y;
        }

        bool intersect = true;
        intersect &= alpha >= 0;
        intersect &= (beta >= 0 && beta <= 1);
        return intersect;
    }
}
