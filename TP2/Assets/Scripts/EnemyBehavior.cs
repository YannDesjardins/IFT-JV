using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBehavior : MonoBehaviour {

    public float speed;
    public float rotationSpeed = 0.01f;
    public EnemyPathfinder pathfinder;
    public PatrolPath patrolPath;
    public float sightRange;

    private GameObject[] players;
    private Vector3 target;

	// Use this for initialization
	void Start () {
        target = Vector3.zero;
        patrolPath = GetComponent<PatrolPath>();
        pathfinder = GetComponent<EnemyPathfinder>();

	}
	
	// Update is called once per frame
	void Update () {
        chooseAction();
        target = FindMovementTarget(target);
        if (target != null)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
               Quaternion.LookRotation(target - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void chooseAction()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        Patrol();
        if (players != null)
        {
            FindPlayerWithinRange();
            if (players.Length > 0)
            {
                patrolPath.StopPatrol();
                FindClosestPlayer();
            }
            else
            {
                Patrol();
            }
        }

    }

    private void FindPlayerWithinRange()
    {
        players = players.Where(p => (p.transform.position - transform.position).magnitude <= sightRange).ToArray();
    }

    private void FindClosestPlayer()
    {
        GameObject min = players[0];
        float minDistance = (min.transform.position - transform.position).magnitude;
        foreach (GameObject go in players)
        {
            float distance = (go.transform.position - transform.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                min = go;
            }
        }
        target = min.transform.position;
    }

    private void Patrol()
    {
        target = patrolPath.Patrol();
    }

    private Vector3 FindMovementTarget(Vector3 target)
    {
        List<Vector3> path = pathfinder.DijkstraFindPath(transform.position, target);
        if (path.Count > 0)
        {
            return pathfinder.DijkstraFindPath(transform.position, target)[0];
        }
        else
        {
            return target;
        }
    }
}
