using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarModifierApplied : MonoBehaviour {

	public GameObject avatarBody;
	public GameObject avatarHead;
	public Material color1;
	public Material color2;
	public Material color3;

	void Start () {

		avatarBody.transform.localScale = StaticGameStats.AvatarBodyScale;
		avatarHead.transform.localScale = StaticGameStats.AvatarHeadScale;


		color1.SetColor("_Color", Color.green);
		color2.SetColor("_Color", Color.green);
		color3.SetColor("_Color", Color.green);

	}
}
