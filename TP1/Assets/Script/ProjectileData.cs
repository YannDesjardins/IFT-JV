using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour {
    public delegate void OnProjectileDestruction();
    public event OnProjectileDestruction onProjectileDestruction;
    public float timeTolive = 4;

    float time;
	// Use this for initialization
	void Start () {
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time-time > 3)
        {
            Destroy(gameObject);
            if (onProjectileDestruction != null)
            {
                onProjectileDestruction();
            }
        }
	}
}
