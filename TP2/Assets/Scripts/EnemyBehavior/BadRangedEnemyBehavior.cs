using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BadRangedEnemyBehavior : EnemyBehavior
{
    public float shootRange = 5;
    public float shootingSpeed = 0.01f;
    public Transform[] bulletSpawn;
    public GameObject bulletPrefab;

    private float lastShot = 0;


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

        enemyAction += RotateTowardTarget;
        enemyAction += ShootTarget;
        //State Check
        if (!FindPlayerWithinRange(shootRange))
        {
            stateAction = ChasePlayer;
        }
    }

    protected void ShootTarget(Vector3 target)
    {
        lastShot -= Time.deltaTime;
        if (lastShot < 0)
        {
            Debug.Log(lastShot);
            lastShot = shootingSpeed;
            Debug.Log(lastShot);
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn[i].position,
            bulletSpawn[i].rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;

            NetworkServer.Spawn(bullet);

            Destroy(bullet, 5.0f);
        }
    }
}
