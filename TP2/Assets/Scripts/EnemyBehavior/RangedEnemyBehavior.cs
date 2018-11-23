using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RangedEnemyBehavior : EnemyBehavior
{
    public float shootRange;
    public float shootingSpeed = 1;
    public Transform[] bulletSpawn;
    public GameObject bulletPrefab;

    private float lastShot = 0;
    
    protected override void ExecuteAction()
    {
        lastShot += Time.deltaTime;

        moveTarget = FindMovementTarget(moveTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                   Quaternion.LookRotation(moveTarget - transform.position), rotationSpeed * Time.deltaTime);

        if (!aggro || (aggro && (target - transform.position).magnitude > 5))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (FindPlayerWithinSight())
        {
            if (lastShot > shootingSpeed)
            {
                if (isServer)
                    CmdFire();
            }
        }
    }

    [Command]
    void CmdFire()
    {
        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            lastShot = 0;
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
