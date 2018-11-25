using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavior : EnemyBehavior
{
    protected override void ChasePlayer()
    {
        //State action
        moveTarget = FindClosestPlayer();
        enemyMovement += MoveTowardTarget;

        if (!AnyPlayerWithinRange(detectionRange))
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
