using UnityEngine;
using System.Collections;

//Base sur http://flattutorials.blogspot.com/2015/02/lets-create-perfect-maze-generator.html#more
// et sur https://www.youtube.com/watch?v=IrO4mswO2o4
public class HuntAndKillMazeAlgorithm
{
    private Cell[,] cells;
    private PseudoRandomNumberGenerator numberGenerator;
    private int lastNumber = 0;
    private int rows;
    private int columns;
    private int currentRow = 0;
    private int currentColumn = 0;
    private bool courseComplete = false;

    public HuntAndKillMazeAlgorithm(Cell[,] mapCells, int PRNGSeed)
    {
        cells = mapCells;
        rows = mapCells.GetLength(0);
        columns = mapCells.GetLength(1);
        numberGenerator = new PseudoRandomNumberGenerator(PRNGSeed);
    }

    public void CreateMap()
    {
        cells[currentRow, currentColumn].visited = true;

        while (!courseComplete)
        {
            Kill();
            Hunt();
        }
    }

    private void Kill()
    {
        while (RouteStillAvailable(currentRow, currentColumn))
        {
            int direction = numberGenerator.GetNextNumber(1,5);
            if (direction != lastNumber)
            {
                cells[currentRow, currentColumn].floor.tag = "Navigation";
            }
            lastNumber = direction;

            if (direction == 1 && CellIsAvailable(currentRow - 1, currentColumn))
            {
                // Nord
                DestroyWallIfItExists(cells[currentRow, currentColumn].northWall);
                DestroyWallIfItExists(cells[currentRow - 1, currentColumn].southWall);
                currentRow--;
            }
            else if (direction == 2 && CellIsAvailable(currentRow + 1, currentColumn))
            {
                // Sud
                DestroyWallIfItExists(cells[currentRow, currentColumn].southWall);
                DestroyWallIfItExists(cells[currentRow + 1, currentColumn].northWall);
                currentRow++;
            }
            else if (direction == 3 && CellIsAvailable(currentRow, currentColumn + 1))
            {
                // est
                DestroyWallIfItExists(cells[currentRow, currentColumn].eastWall);
                DestroyWallIfItExists(cells[currentRow, currentColumn + 1].westWall);
                currentColumn++;
            }
            else if (direction == 4 && CellIsAvailable(currentRow, currentColumn - 1))
            {
                // ouest
                DestroyWallIfItExists(cells[currentRow, currentColumn].westWall);
                DestroyWallIfItExists(cells[currentRow, currentColumn - 1].eastWall);
                currentColumn--;
            }

            cells[currentRow, currentColumn].visited = true;
        }
        
    }

    private void Hunt()
    {
        courseComplete = true;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                if (!cells[r, c].visited && CellHasAnAdjacentVisitedCell(r, c))
                {
                    courseComplete = false;
                    currentRow = r;
                    currentColumn = c;
                    DestroyAdjacentWall(currentRow, currentColumn);
                    cells[currentRow, currentColumn].visited = true;
                    return;
                }
            }
        }
    }


    private bool RouteStillAvailable(int row, int column)
    {
        int availableRoutes = 0;

        if (row > 0 && !cells[row - 1, column].visited)
        {
            availableRoutes++;
        }
        else if(row < rows - 1 && !cells[row + 1, column].visited)
        {
            availableRoutes++;
        }
        else if (column > 0 && !cells[row, column - 1].visited)
        {
            availableRoutes++;
        }
        else if (column < columns - 1 && !cells[row, column + 1].visited)
        {
            availableRoutes++;
        }

        return availableRoutes > 0;
    }

    private bool CellIsAvailable(int row, int column)
    {
        return (row >= 0 && row < rows && column >= 0 && column < columns && !cells[row, column].visited);
    }

    private void DestroyWallIfItExists(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

    private bool CellHasAnAdjacentVisitedCell(int row, int column)
    {
        int visitedCells = 0;

        if (row > 0 && cells[row - 1, column].visited)
        {
            visitedCells++;
        }
        else if (row < (rows - 2) && cells[row + 1, column].visited)
        {
            visitedCells++;
        }
        else if (column > 0 && cells[row, column - 1].visited)
        {
            visitedCells++;
        }
        else if (column < (columns - 2) && cells[row, column + 1].visited)
        {
            visitedCells++;
        }

        return visitedCells > 0;
    }

    private void DestroyAdjacentWall(int row, int column)
    {
        bool wallDestroyed = false;

        while (!wallDestroyed)
        {
            int direction = numberGenerator.GetNextNumber(1,5);

            if (direction == 1 && row > 0 && cells[row - 1, column].visited)
            {
                DestroyWallIfItExists(cells[row, column].northWall);
                DestroyWallIfItExists(cells[row - 1, column].southWall);
                wallDestroyed = true;
            }
            else if (direction == 2 && row < (rows - 2) && cells[row + 1, column].visited)
            {
                DestroyWallIfItExists(cells[row, column].southWall);
                DestroyWallIfItExists(cells[row + 1, column].northWall);
                wallDestroyed = true;
            }
            else if (direction == 3 && column > 0 && cells[row, column - 1].visited)
            {
                DestroyWallIfItExists(cells[row, column].westWall);
                DestroyWallIfItExists(cells[row, column - 1].eastWall);
                wallDestroyed = true;
            }
            else if (direction == 4 && column < (columns - 2) && cells[row, column + 1].visited)
            {
                DestroyWallIfItExists(cells[row, column].eastWall);
                DestroyWallIfItExists(cells[row, column + 1].westWall);
                wallDestroyed = true;
            }
        }

    }

}
