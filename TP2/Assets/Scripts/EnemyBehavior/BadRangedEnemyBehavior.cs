﻿using System.Collections;
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
    }

    void AttackPlayer()
    {
        //State action
        target = FindClosestPlayer();
        moveTarget = target;

        enemyMovement += RotateTowardTarget;
        enemyAction += ShootTarget;
        //State Check
        if (!FindPlayerWithinRange(shootRange))
        {
            stateAction = ChasePlayer;
        }
    }
}