using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BadRangedEnemyBehavior : RangedEnemyBehavior
{

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

        if (playerVisible && playerWithinShootRange)
        {
            stateAction = AttackPlayer;
        }
        else if (!playerVisible || !playerWithinSightRange)
        {
            if (IsHidden())
            {
                enemiesSituation.DecreaseAlertedEnemies();
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
        target = FindClosestPlayer();
        moveTarget = target;

        enemyMovement += RotateTowardTarget;
        enemyAction += ShootTarget;
        //State Check
        if (!(FindPlayerVisible() && AnyPlayerWithinRange(shootRange)))
        {
            stateAction = ChasePlayer;
        }
    }
}
