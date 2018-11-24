using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AdvancedRangedEnemyBehavior : RangedEnemyBehavior
{
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
        if (FindPlayerVisible() && FindPlayerWithinRange(shootRange))
        {
            stateAction = AttackPlayer;
        }
        if (!FindPlayerVisible() || !FindPlayerWithinRange(detectionRange))
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
        if (IsLowOnHealth())
        {
            stateAction = PlaySafe;
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
        enemyAction += ShootTarget;
        //State Check

        if (IsLowOnHealth())
        {
            stateAction = PlaySafe;
        }
        if (!(FindPlayerVisible() && FindPlayerWithinRange(shootRange)))
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

    protected void StrafeAround(Vector3 position)
    {
        transform.position += transform.right * speed / 1.5f * Time.deltaTime;
    }
}
