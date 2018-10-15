using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {

    public GameObject projectile;
    public float time;
	// Use this for initialization
	void Start () {
        time = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - time > 1)
        {
            createProjectile();
            time = Time.time;
        }
    }

    public void createProjectile()
    {
        GameObject projectileInstance = Instantiate(projectile, transform);
        MoveWithForce forceMove = projectileInstance.GetComponent<MoveWithForce>();
        float adjust = transform.position.x > 0 ? -1 : 1;
        float x = Random.Range(adjust*100, adjust*400);
        float y = Random.Range(30, 150);
        float z = Random.Range(-400, 400);
        forceMove.ApplyForce(new Vector3(x, y, z), 0.5f);
    }
}
