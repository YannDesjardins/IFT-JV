using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioriCollisionDetection : AbstractCollider {

    private GameObject[] walls;

    new void Start()
    {
        base.Start();
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    void FixedUpdate () {
        //GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        //verifier collision avec tous les object avec lesquelles il peut y avoir collision.
        for(int i = 0; i < walls.Length; i++)
        {
            for (int j = 0; j < vertices.Length; j++)
            {
                Vector3 globalPointPos = transform.TransformPoint(vertices[j]);
                if (CollisionPoint(globalPointPos,walls[i]))
                {
                    wallCollisionReaction();
                    break;
                }

            }
        }

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
            CalculateRebound(n);
            return true;

        }
        return false;
    }

}
