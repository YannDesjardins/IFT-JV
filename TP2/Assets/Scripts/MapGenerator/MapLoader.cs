using UnityEngine;
using System.Collections;

//Base sur http://flattutorials.blogspot.com/2015/02/lets-create-perfect-maze-generator.html#more
// et sur https://www.youtube.com/watch?v=IrO4mswO2o4
public class MapLoader : MonoBehaviour
{

    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject props1;
    [SerializeField] private GameObject props2;
    [SerializeField] private GameObject props3;

    private float size = 8f;
    private Cell[,] cells;
    private PseudoRandomNumberGenerator numberGenerator;

    // Use this for initialization
    void Start()
    {
        numberGenerator = new PseudoRandomNumberGenerator(StaticGameStats.Seed);
        Initialize();
        HuntAndKillMazeAlgorithm ma = new HuntAndKillMazeAlgorithm(cells, StaticGameStats.Seed);
        ma.CreateMap();
    }
    // Update is called once per frame
    void Update()
    {
    }


    private void Initialize()
    {
        cells = new Cell[StaticGameStats.Rows, StaticGameStats.Columns];

        for (int r = 0; r < StaticGameStats.Rows; r++)
        {
            for (int c = 0; c < StaticGameStats.Columns; c++)
            {
                cells[r, c] = new Cell();

                cells[r, c].floor = Instantiate(floor, new Vector3(r * size, -(1.3f), c * size), Quaternion.identity) as GameObject;
                cells[r, c].floor.name = "Floor " + r + "," + c;
                cells[r, c].floor.transform.Rotate(Vector3.right, 90f);


                if (c == 0)
                {
                    cells[r, c].westWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    cells[r, c].westWall.name = "West Wall " + r + "," + c;
                }

                cells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                cells[r, c].eastWall.name = "East Wall " + r + "," + c;

                if (r == 0)
                {
                    cells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    cells[r, c].northWall.name = "North Wall " + r + "," + c;
                    cells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

                cells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                cells[r, c].southWall.name = "South Wall " + r + "," + c;
                cells[r, c].southWall.transform.Rotate(Vector3.up * 90f);

                int propsSpawn = numberGenerator.GetNextNumber(1,9);
                if (propsSpawn<4)
                {
                    cells[r, c].props = Instantiate(GetProps(propsSpawn),
                        new Vector3((r * size)- Random.Range(-(size / 2f),size /2f), -(1f), (c * size) - Random.Range(-(size / 2f), size / 2f)),
                        Quaternion.Euler(0.0f,Random.Range(0, 180),0.0f)) as GameObject;
                    cells[r, c].props.name = "Props " + r + "," + c;
                }
            }
        }
    }

    private GameObject GetProps(int number)
    {
        if(number == 1)
        {
            return props1;
        }
        else if (number == 2)
        {
            return props2;
        }
        else
        {
            return props3;
        }

    }

    private void GetPropsPosition(int number)
    {
        if (number == 1)
        {

        }
        else if (number == 2)
        {

        }
        else
        {

        }

    }
}

