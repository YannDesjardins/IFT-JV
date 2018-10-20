using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour {

    private float width;
    private float heigth;
    private float depth;

    private Vector3 min;
    private Vector3 max;

    // Use this for initialization
    void Start () {
        //initialisation des proprietes des limites du niveau
        width = gameObject.transform.localScale.x;
        heigth = gameObject.transform.localScale.y;
        depth = gameObject.transform.localScale.z;

        GetMin();
        GetMax();

    }

    // regarde si le point est dans les limites du AABB, retoune true si oui, false autrement
    public bool IsOutOfBound(Vector3 point)
    {
        return !(point.x > min.x && point.x < max.x &&
            point.y > min.y && point.y < max.y &&
            point.z > min.z && point.z < max.z);
    }

    //calcul le point minimum du AABB selon les coordonnes de main droite de unity
    //et le point central de la bounding box
    void GetMin()
    {
        min = new Vector3(
            gameObject.transform.position.x - width/2,
            gameObject.transform.position.y - heigth/2,
            gameObject.transform.position.z - depth/2);

    }

    //calcul le point maximum du AABB selon les coordonnes de main droite de unity
    //et le point central de la bounding box
    void GetMax()
    {
        max = new Vector3(
            gameObject.transform.position.x + width / 2,
            gameObject.transform.position.y + heigth / 2,
            gameObject.transform.position.z + depth / 2);

    }

}
