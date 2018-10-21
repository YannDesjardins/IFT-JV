using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour {
    public delegate void OnProjectileDestruction();
    public event OnProjectileDestruction onProjectileDestruction;
    private AABB aabb;

	// Use this for initialization
	void Start () {
        GameObject bounds = GameObject.FindWithTag("Bounds");

        if (bounds != null)
        {
            aabb = bounds.GetComponent<AABB>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (aabb.IsOutOfBound(gameObject.transform.position))
        {
            Destroy(gameObject);
            if (onProjectileDestruction != null)
            {
                onProjectileDestruction();
            }
        }
	}
}
