using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AdvancedRangedEnemyBehavior : RangedEnemyBehavior
{
    public float playSafeTime = 1;

    private GameObject lastPlayerTargeted;
    private Vector3 lastPlayerPosition;
    private float timeBacking = 0;

    protected override void ChasePlayer()
    {
        //State action
        target = FindClosestPlayer();
        moveTarget = target;

        enemyAction += MoveTowardTarget;
        if (IsPlayerWithinSight())
        {
            enemyMovement += ShootTarget;
        }

        //State Check
        bool playerVisible = FindPlayerVisible();
        bool playerWithinShootRange = AnyPlayerWithinRange(shootRange);
        bool playerWithinSightRange = AnyPlayerWithinRange(detectionRange);

        if (IsLowOnHealth())
        {
            stateAction = PlaySafe;
        }
        else if (playerVisible && playerWithinShootRange)
        {
            stateAction = AttackPlayer;
        }
        else if (!playerVisible || !playerWithinSightRange)
        {
            if (IsHidden())
            {
                stateAction = Patrol;
            }
        }
        else
        {
            timeHidden = 0;
        }
        
    }

    void AttackPlayer()
    {
        //State action
        GameObject player;
        moveTarget=FindClosestPlayer(out player);
        enemyMovement += StrafeAround;

        target = FindPotentialPosition(player);
        enemyAction += RotateTowardTarget;
        if (!VerifyAllyInShot())
        {
            enemyAction += ShootTarget;
        }
       
        //State Check

        if (IsLowOnHealth())
        {
            stateAction = PlaySafe;
        }
        else if (!(FindPlayerVisible() && AnyPlayerWithinRange(shootRange)))
        {
            stateAction = ChasePlayer;
        }
    }

    void PlaySafe()
    {
        //State Action
        target = FindClosestPlayer();
        moveTarget = target;

        enemyMovement = MoveBackward;
        if (IsPlayerWithinSight())
        {
            enemyAction += ShootTarget;
        }
        timeBacking += Time.deltaTime;

        //State Check
        if (!IsLowOnHealth())
        {
            stateAction = Patrol;
            timeBacking = 0;
        }
    }

    protected Vector3 FindPotentialPosition(GameObject player)
    {
        Vector3 position;
        if(player == lastPlayerTargeted)
        {
            Vector3 direction = (player.transform.position - lastPlayerPosition).normalized;
            Vector3 estimatedPosition = player.transform.position + direction * 100 * Time.deltaTime;
            position = estimatedPosition;
        }
        else
        {
            position = player.transform.position;
        }

        lastPlayerPosition = player.transform.position;
        lastPlayerTargeted = player;
        return position;
    }

    protected bool VerifyAllyInShot()
    {
        bool allyInShot = false;
        RaycastHit hit;
        Vector3 direction = (target - transform.position).normalized;
        allyInShot |= Physics.Raycast(transform.position, direction, out hit);
        if (allyInShot)
        {
            allyInShot &= hit.collider.tag == "Enemy";
        }
        return allyInShot;
    }

    protected void StrafeAround(Vector3 position)
    {
        transform.position += transform.right * speed / 1.5f * Time.deltaTime;
    }

    protected void FindSafestEscapeRoute()
    {
        Vector3 playerPosition = FindClosestPlayer();
        GameObject[] possiblePoints = GameObject.FindGameObjectsWithTag("Navigation");
    }

    protected void FindVisibleEscape(GameObject[] points)
    {

    }
}
