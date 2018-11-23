using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public abstract class EnemyBehavior : NetworkBehaviour
{

    public float speed;
    public float rotationSpeed = 2f;
    public float sightRange;
    public float aggroTime;

    protected EnemyPathfinder pathfinder;
    protected PatrolPath patrolPath;
    protected float inRangeTime = 0;
    protected float outOfRangeTime = 0;
    protected bool aggro;
    protected GameObject[] players;
    protected Vector3 target;
    protected Vector3 moveTarget;
    protected GameObject targetPlayer;


    // Use this for initialization
    void Start()
    {
        aggro = false;
        patrolPath = GetComponent<PatrolPath>();
        pathfinder = GetComponent<EnemyPathfinder>();
    }

    // Update is called once per frame
    void Update()
    {
        ChooseAction();
        ExecuteAction();
    }

    protected abstract void ExecuteAction();

    protected void ChooseAction()
    {
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players != null)
            {
                FindPlayerWithinRange();
                if (aggro)
                {
                    patrolPath.StopPatrol();
                    target = FindClosestPlayer();
                    moveTarget = target;
                }
                else
                {
                    moveTarget = patrolPath.Patrol();
                }
            }
        }
    }


    protected void FindPlayerWithinRange()
    {
        int playerInRange = players.Where(p => (p.transform.position - transform.position).magnitude <= sightRange).ToArray().Length;
        if (playerInRange > 0 && !aggro)
        {
            inRangeTime += Time.deltaTime;
            float timeDiff = outOfRangeTime - Time.deltaTime;
            outOfRangeTime = timeDiff > 0 ? timeDiff : 0;
            if (inRangeTime > aggroTime)
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
            if (outOfRangeTime > aggroTime)
            {
                aggro = false;
                outOfRangeTime = 0;
            }
        }
    }

    protected Vector3 FindClosestPlayer()
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
                targetPlayer = go;
            }
        }
        return min.transform.position;
    }

    protected Vector3 FindMovementTarget(Vector3 t)
    {
        List<Vector3> path = pathfinder.DijkstraFindPath(transform.position, t);
        if (path.Count > 0)
        {
            return pathfinder.DijkstraFindPath(transform.position, t)[0];
        }
        else
        {
            return t;
        }

    }

    protected bool FindPlayerWithinSight()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        return hit.collider.tag == "Player";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(moveTarget, 1);

    }
}
