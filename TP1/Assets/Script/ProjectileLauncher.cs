using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {
    public delegate void OnProjectileDesctruction();
    public event OnProjectileDesctruction onProjectileDestruction;
    public delegate void OnWallCollision();
    public event OnWallCollision onWallCollision;

    public GameObject projectile;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void createProjectile()
    {
        GameObject projectileInstance = Instantiate(projectile, transform);
        projectileInstance.GetComponent<ProjectileData>().onProjectileDestruction += OnProjectileDestructionCall;
        projectileInstance.GetComponent<AbstractCollider>().onWallCollision += onWallCollisionCall;
        MoveWithForce forceMove = projectileInstance.GetComponent<MoveWithForce>();
        float adjust = transform.position.x > 0 ? -1 : 1;
        float x = Random.Range(adjust*100, adjust*400);
        float y = Random.Range(30, 150);
        float z = Random.Range(-400, 400);
        forceMove.ApplyForce(new Vector3(x, y, z), 0.5f);
    }

    public void OnProjectileDestructionCall()
    {
        if (onProjectileDestruction != null)
        {
            onProjectileDestruction();
        }
    }

    public void onWallCollisionCall()
    {
        if (onWallCollision != null)
        {
            onWallCollision();
        }
    }
}
