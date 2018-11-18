using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyMovement : NetworkBehaviour {




	// Use this for initialization
	void Start () {
		
	
	}



	// Update is called once per frame
	void Update () {


		transform.Translate(0.1f, 0, 0, Space.World);
		transform.Translate(0, 0, 0.1f, Space.World);
		transform.RotateAround (new Vector3 (-5, 0, 5), new Vector3 (0, 1, 0), 100 * Time.deltaTime);

	}
		
}

