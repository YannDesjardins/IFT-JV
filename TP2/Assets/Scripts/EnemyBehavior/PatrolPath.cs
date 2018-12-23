using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour {
    public Vector3 begin;
    public Vector3 end;

    private List<Vector3> patrolPoints = new List<Vector3>();
    private int currentIndex = 0;
    private Vector3 currentPosition;
    private Vector3 currentDestination;
    private bool patrolling;
    private bool pursuing;
    private float distancePatrol = 1;

    public void Start()
    {
        GameObject[] patrolObjects = GameObject.FindGameObjectsWithTag("Patrol");
        foreach(GameObject patrolObject in patrolObjects)
        {
            patrolPoints.Add(patrolObject.transform.position);
        }
        patrolling = true;
        currentDestination = end;
        currentPosition = transform.position;
    }

    public void StopPatrol()
    {
        patrolling = false;
    }

    public Vector3 Patrol()
    {
        if (patrolling)
        {
            return currentDestination;
        }
        else
        {
            return currentPosition;
        }
    }

    public void Update()
    {
        if (patrolling)
        {
            float distance = (currentDestination - transform.position).magnitude;
            if (distance < distancePatrol)
            {
                currentIndex++;
                if (currentIndex > patrolPoints.Count)
                {
                    currentIndex = 0;
                }
                currentDestination = patrolPoints[currentIndex];
            }
            currentPosition = transform.position;
        }
        else
        {
            float distance = (currentPosition - transform.position).magnitude;
            if(distance < distancePatrol)
            {
                patrolling = true;
            }

        }
    }

    public Vector3 GetCurrentDestination()
    {
        return currentDestination;
    }

    public Vector3 GetCurrentPosition()
    {
        return currentPosition;
    }
}
