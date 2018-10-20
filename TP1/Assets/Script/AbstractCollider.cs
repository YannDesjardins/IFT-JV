using UnityEngine;
using System.Collections;

public abstract class AbstractCollider : MonoBehaviour
{
    protected Physics physics;
    public delegate void OnWallCollision();
    public event OnWallCollision onWallCollision;


    protected void Start()
    {
        physics = GetComponent<Physics>();
    }

    public float reboundFactor = 1f;

    protected bool VerifyIfWithinPolygon(Vector3[] vertices, Vector3 point)
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

    protected bool IntersectionVecteur2d(Vector3 p, Vector3 v, Vector3 q, Vector3 w)
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

    //Verifie si l'axe des x et y de du point d'intersection font partie du quad de collision.
    //source: http://wiki.unity3d.com/index.php/PolyContainsPoint 
    bool ContainsPoint(Vector3[] polyPoints, Vector3 p)
    {
        int j = polyPoints.Length - 1;
        bool inside = false;
        for (int i = 0; i < polyPoints.Length; j = i++)
        {
            if (((polyPoints[i].y <= p.y && p.y < polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y < polyPoints[i].y)) &&
                (p.x < (polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x))
            {
                inside = !inside;
            }
        }
        return inside;
    }

    protected void CalculateRebound(Vector3 planeNormal)
    {
        Vector3 vel = new Vector3(physics.getVelocity().x, physics.getVelocity().y, physics.getVelocity().z);
        physics.StopObject();
        vel = vel - reboundFactor * (2 * (Vector3.Dot(planeNormal, vel) * planeNormal));
        physics.setVelocity(vel);
    }

    protected void wallCollisionReaction()
    {
        if (onWallCollision != null)
        {
            onWallCollision();
        }
    }
}
