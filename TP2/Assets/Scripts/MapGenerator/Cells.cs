using UnityEngine;

//Base sur http://flattutorials.blogspot.com/2015/02/lets-create-perfect-maze-generator.html#more
// et sur https://www.youtube.com/watch?v=IrO4mswO2o4
public class Cell
{
    public bool visited = false;

    public GameObject northWall;
    public GameObject southWall;
    public GameObject eastWall;
    public GameObject westWall;
    public GameObject floor;
}