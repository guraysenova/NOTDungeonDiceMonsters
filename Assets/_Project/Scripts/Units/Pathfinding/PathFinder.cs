/*
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
/   TO-DO : Add Portal Logic
*/
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    GridNode[,] grid;

    TwoDCoordinate boardSize = new TwoDCoordinate();

    public static PathFinder instance;

    [SerializeField]
    List<GridNode> path = new List<GridNode>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public List<GridNode> GetPath(TwoDCoordinate startPos , TwoDCoordinate endPos)
    {
        UpdateGrid(BoardManager.instance.GetTileData(), boardSize);
        grid[startPos.x / 2, startPos.y / 2].isVisited = 0;
        SetDistance(startPos.x, startPos.y);
        SetPath(endPos.x, endPos.y);

        path.Reverse();

        return path;
    }

    public void SetGrid(List<TileData> tileData , TwoDCoordinate boardSize)
    {
        this.boardSize = boardSize;
        grid = new GridNode[boardSize.x, boardSize.y];
        UpdateGrid(tileData, boardSize);
    }

    public void UpdateGrid(List<TileData> tileData, TwoDCoordinate boardSize)
    {
        int index = 0;
        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                if (tileData[index].isFilled)
                {
                    GridNode node = new GridNode();
                    node.x = tileData[index].coordinates.x / 2;
                    node.y = tileData[index].coordinates.y / 2;
                    node.isVisited = -1;
                    grid[x, y] = node;
                }
                else
                {
                    grid[x, y] = null;
                }
                index++;
            }
        }
    }

    void SetPath(int targetX , int targetY)
    {
        int x = targetX / 2;
        int y = targetY / 2;
        int step;

        List<GridNode> tempList = new List<GridNode>();

        path.Clear();

        if(grid[x , y] != null && grid[x ,y].isVisited > 0)
        {
            path.Add(grid[x, y]);
            step = grid[x, y].isVisited - 1;
        }
        else
        {
            Debug.Log("no path");
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            // TO-DO : SHOW UI ERROR
            return;
        }

        for (int i = step; step> -1; step--)
        {
            if(TestDirection(x, y, step , Direction.Up))
            {
                tempList.Add(grid[x, y + 1]);
            }
            if (TestDirection(x, y, step, Direction.Down))
            {
                tempList.Add(grid[x, y - 1]);
            }
            if (TestDirection(x, y, step, Direction.Left))
            {
                tempList.Add(grid[x - 1, y]);
            }
            if (TestDirection(x, y, step, Direction.Right))
            {
                tempList.Add(grid[x + 1, y]);
            }
            GridNode tempNode = FindClosest(grid[targetX / 2, targetY / 2].x, grid[targetX / 2, targetY / 2].y, tempList);
            path.Add(tempNode);
            x = tempNode.x;
            y = tempNode.y;
            tempList.Clear();
        }

        foreach (var item in path)
        {
            Debug.Log(item.x + " " + item.y + " " + item.isVisited);
        }
    }

    GridNode FindClosest(int x , int y , List<GridNode> nodes)
    {
        float currentDistance = 999999f;
        int indexNumber = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            if(Vector2.Distance(new Vector2(x , y) , new Vector2(nodes[i].x , nodes[i].y)) < currentDistance)
            {
                currentDistance = Vector2.Distance(new Vector2(x, y), new Vector2(nodes[i].x, nodes[i].y));
                indexNumber = i;
            }
        }
        return nodes[indexNumber];
    }

    void SetVisited(int x , int y , int step)
    {
        if (grid[x, y] != null)
        {
            grid[x, y].isVisited = step;
        }
    }

    void SetDistance(int startX , int startY)
    {
        //int[] testArray = new int[boardSize.x * boardSize.y];
        for (int step = 1; step < boardSize.x * boardSize.y; step++)
        {
            foreach (GridNode node in grid)
            {
                if(node != null && node.isVisited == step - 1)
                {
                    TestFourDirections(node.x, node.y, step);
                }
            }
        }
    }

    bool TestDirection(int x , int y , int step , Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                if(y + 1 < boardSize.y && grid[x,y + 1] != null && grid[x,y + 1].isVisited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Direction.Down:
                if (y - 1 > -1 && grid[x, y - 1] != null && grid[x, y - 1].isVisited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Direction.Left:
                if (x - 1 > -1 && grid[x - 1, y] != null && grid[x - 1, y].isVisited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Direction.Right:
                if (x + 1 < boardSize.x && grid[x + 1, y] != null && grid[x + 1, y].isVisited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }

    void TestFourDirections(int x , int y , int step)
    {
        if(TestDirection(x , y , -1 , Direction.Up))
        {
            SetVisited(x, y + 1, step);
        }
        if (TestDirection(x, y, -1, Direction.Down))
        {
            SetVisited(x, y - 1, step);
        }
        if (TestDirection(x, y, -1, Direction.Left))
        {
            SetVisited(x - 1, y , step);
        }
        if (TestDirection(x, y, -1, Direction.Right))
        {
            SetVisited(x + 1, y, step);
        }
    }
}