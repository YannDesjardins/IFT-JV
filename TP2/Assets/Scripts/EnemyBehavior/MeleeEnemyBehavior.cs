using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavior : EnemyBehavior
{

    protected override void ExecuteAction()
    {
        moveTarget = FindMovementTarget(target);
        transform.rotation = Quaternion.Slerp(transform.rotation,
               Quaternion.LookRotation(moveTarget - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
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
