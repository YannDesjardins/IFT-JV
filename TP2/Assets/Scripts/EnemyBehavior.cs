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

    private float inRangeTime=0;
    private float outOfRangeTime=0;
    private bool aggro;
    private GameObject[] players;
    private Vector3 target;


	// Use this for initialization
	void Start () {
        aggro = false;
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

        if (players != null)
        {
            FindPlayerWithinRange();
            if (aggro)
            {
                patrolPath.StopPatrol();
                FindClosestPlayer();
            }
            else
            {
                Patrol();
            }
        }
        Debug.Log("TARGET:" + target);
    }

    private void FindPlayerWithinRange()
    {
        int playerInRange = players.Where(p => (p.transform.position - transform.position).magnitude <= sightRange).ToArray().Length;
        if (playerInRange > 0 && !aggro)
        {
            inRangeTime += Time.deltaTime;
            float timeDiff = outOfRangeTime - Time.deltaTime;
            outOfRangeTime = timeDiff > 0 ? timeDiff : 0;
            if(inRangeTime > 2)
            {
                aggro = true;
                inRangeTime = 0;
            }
        }
        else if (playerInRange == 0 && aggro)
        {
            outOfRangeTime += Time.deltaTime;
            float timeDiff = inRangeTime - Time.deltaTime;
            inRangeTime = timeDiff > 0 ? timeDiff : 0;
            if (outOfRangeTime > 2)
            {
                aggro = false;
                outOfRangeTime = 0;
            }
        }
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

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        if (hit.tag == "Player")
        {
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);
            }
        }
    }
}
