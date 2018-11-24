using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RangedEnemyBehavior : EnemyBehavior
{
    public float shootRange = 20;
    public float fleeingRange = 15;
    public float shootingSpeed = 0.5f;
    public Transform[] bulletSpawn;
    public GameObject bulletPrefab;

    private float lastShot = 0;

    protected new void Start()
    {
        base.Start();
        bulletSpawn = new Transform[2];
        bulletSpawn[0] = gameObject.GetComponentInChildren<Transform>().Find("BulletSpawn1");
        bulletSpawn[1] = gameObject.GetComponentInChildren<Transform>().Find("BulletSpawn2");
    }

    protected override void ChasePlayer()
    {

    }

    protected void ShootTarget(Vector3 target)
    {
        lastShot += Time.deltaTime;
        if (lastShot > shootingSpeed)
        {
            lastShot = 0;
            CmdFire();
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
