using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour {
    public delegate void OnProjectileDestruction();
    public event OnProjectileDestruction onProjectileDestruction;
    public float timeTolive = 4;
    private AABB aabb;

    float time;
	// Use this for initialization
	void Start () {
        time = Time.time;

        GameObject bounds = GameObject.FindWithTag("Bounds");

        if (bounds != null)
        {
            aabb = bounds.GetComponent<AABB>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time-time > 3 || aabb.IsOutOfBound(gameObject.transform.position))
        {
            Destroy(gameObject);
            if (onProjectileDestruction != null)
            {
                onProjectileDestruction();
            }
        }
	}
}
