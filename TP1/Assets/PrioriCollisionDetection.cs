using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioriCollisionDetection : MonoBehaviour {
	Vector3 rayLength;
	Vector3 rayDirection;
	Vector3 currentPos;
	Vector3 planeNormal = Vector3.zero;
	Vector3 planePoint = Vector3.zero;

    public float reboundFactor = 1f;

	//CollisionPlane wall;
	private Physics physics;


	// Use this for initialization
	void Start () {
		//wall = GameObject.Find("Collision").GetComponent(typeof(CollisionPlane)) as CollisionPlane;
		physics = GetComponent<Physics> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        //verifier collision avec tous les object avec lesquelles il peut y avoir collision.
        for(int i = 0; i < walls.Length; i++)
        {
            for (int j = 0; j < vertices.Length; j++)
            {
                Vector3 globalPointPos = transform.TransformPoint(vertices[j]);
                if (CollisionPoint(globalPointPos,walls[i]))
                {
                    break;
                }

            }
        }

        
		//GetPlanePointAndNormal ();
		//RayTraceForward ();
		//CalculateCollisionLinePlane ();
	}

	//void GetPlanePointAndNormal (){
		
	//	planeNormal = wall.getPlaneNormal();
	//	planePoint = wall.getPlanePoint();

	//}

	void RayTraceForward () {

		currentPos = transform.position;
		rayLength = transform.forward * 50 + currentPos;

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, currentPos);
		lineRenderer.SetPosition(1, rayLength);

		rayDirection = (rayLength - currentPos).normalized;
		
	}
		

	//void CalculateCollisionLinePlane ()
	//{
	//	float length;
	//	float dotProduct1;
	//	float dotProduct2;
	//	Vector3 intersection;
	//	Vector3 objectSize = GetComponent<Renderer> ().bounds.size/2;
	//	float objectSizeZ = objectSize.z;

	//	currentPos.z = currentPos.z + objectSizeZ;
	//	dotProduct1 = Vector3.Dot ((currentPos - planePoint), planeNormal);
	//	dotProduct2 = Vector3.Dot (rayDirection, planeNormal);

	//	length = dotProduct1 / dotProduct2;
	//	intersection = rayDirection - rayDirection * length;

	//	intersection = transform.TransformPoint (intersection);
	//	//Debug.Log (intersection);

	//	Vector3[] planePoints = wall.getPlanePoints ();
			
	//	bool pointInsidePlane = ContainsPoint (planePoints, intersection);

	//	float distanceCollision = (currentPos - intersection).magnitude;


	//	if (distanceCollision < 1f && pointInsidePlane == true) {
	//		//Debug.Log ("Collision!");

	//		physics.StopObject ();

	//		Vector3 tempPos = transform.position;
	//		tempPos.z = tempPos.z - 0.1f;
	//		transform.position = tempPos;
	//	}

	//}

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
		

    bool CollisionPoint(Vector3 point, GameObject wall)
    {
        Vector3 movementVector = physics.getMovement();
        //Regarder si movementVector intersect avec plane
        return VectorIntersectPlane(movementVector, point, wall.transform.GetChild(0).GetComponent<CollisionPlane>());

    }
    
    bool VectorIntersectPlane(Vector3 v, Vector3 p, CollisionPlane wall)
    {

        //regarder si intersection entre rayon et plane        
        float d = Vector3.Dot(wall.getPlanePoints()[0], wall.getPlaneNormal());
        Vector3 n = wall.getPlaneNormal();

        //resoudre system equation entre droite du vector et plane de la face
        float alpha = (d - Vector3.Dot(n, p)) / Vector3.Dot(n, v);
        //regarder si intersection est dans vecteur
        if (alpha > 1 || alpha < -1)
        {
            return false;
        }
        //regarder si intersection est dans polygon
        Vector3 planeIntersection = p + v * alpha;
        
        planeIntersection = wall.transform.InverseTransformPoint(planeIntersection);
        if (VerifyIfWithinPolygon(wall.getPlanePointsLocal(), planeIntersection)){
            Debug.Log("COLLISION at point:"+p);
            Vector3 vel = new Vector3(physics.getVelocity().x,physics.getVelocity().y,physics.getVelocity().z);
            physics.StopObject();
            vel = vel - reboundFactor * (2 * (Vector3.Dot(n, vel) * n));
            physics.setVelocity(vel);
            return true;

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
            if (IntersectionVecteur2d(point, ray, vertices[j], vertices[i] - vertices[j])){
                intersection++;
            }
        }
        return intersection%2==1;
    }

    bool IntersectionVecteur2d(Vector3 p,Vector3 v, Vector3 q, Vector3 w)
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
