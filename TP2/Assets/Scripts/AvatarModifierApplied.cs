using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarModifierApplied : MonoBehaviour {

	public GameObject avatarBody;
	public GameObject avatarHead;
	public Material color1;
	public Material color2;
	public Material color3;

	public GameObject avatarSantaHat;

	void Start () {

		avatarBody.transform.localScale = StaticGameStats.AvatarBodyScale;
		avatarHead.transform.localScale = StaticGameStats.AvatarHeadScale;
		avatarSantaHat.active = StaticGameStats.AvatarSantaHat;
		if (StaticGameStats.AvatarSantaHatCombined == true) {
			CombineSantaHat ();

		}
		ChangeColor (color1, StaticGameStats.AvatarColor1);
		ChangeColor (color2, StaticGameStats.AvatarColor2);
		ChangeColor (color3, StaticGameStats.AvatarColor3);

	}

	void CombineSantaHat(){
		CombineInstance[] combine = new CombineInstance[2];
		combine [0].mesh = avatarHead.GetComponent<MeshFilter>().sharedMesh;
		combine [1].mesh = avatarSantaHat.GetComponent<MeshFilter>().sharedMesh;
		Mesh avatarCombined = new Mesh();
		avatarCombined.CombineMeshes (combine, false, false);
		avatarHead.GetComponent<MeshFilter> ().sharedMesh = avatarCombined;
	}

	void ChangeColor(Material color, int newcolor){
		if (newcolor == 0){
			color.SetColor("_Color", Color.white);
		}
		else if (newcolor == 1){
			color.SetColor("_Color", Color.black);
		}
		else if (newcolor == 2){
			color.SetColor("_Color", Color.green);
		}
		else if (newcolor == 3){
			color.SetColor("_Color", Color.blue);
		}
		else if (newcolor == 4){
			color.SetColor("_Color", Color.yellow);
		}
	}
}
