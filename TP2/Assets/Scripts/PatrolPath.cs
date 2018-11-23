using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour {
    public Vector3 begin;
    public Vector3 end;

    private Vector3 currentPosition;
    private Vector3 currentDestination;
    private bool patrolling;
    private bool pursuing;
    private float distancePatrol = 1;

    public void Start()
    {
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
            if(distance < distancePatrol)
            {
                //changing patrol target
                if (currentDestination.Equals(begin)) { currentDestination = end; }
                else { currentDestination = begin; }
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
