using System;
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
        UpdateGrid(BoardManager.instance.GetTileData(), boardSize , endPos);

        GridNode startNode = grid[startPos.x / 2, startPos.y / 2];
        GridNode endNode = grid[endPos.x / 2, endPos.y / 2];

        List<GridNode> openSet = new List<GridNode>();
        HashSet<GridNode> closedSet = new HashSet<GridNode>();

        /*grid[startPos.x / 2, startPos.y / 2].globalGoal = 0;
        grid[startPos.x / 2, startPos.y / 2].localGoal = Mathf.Abs((startPos.x / 2) - (endPos.x / 2)) + Mathf.Abs((startPos.y / 2) - (endPos.y / 2));

        endNode.localGoal = 0;*/


        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            GridNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost))
                {
                    if(openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            Debug.Log(currentNode.nodeCoordinates.x.ToString() + " " + currentNode.nodeCoordinates.y.ToString());
            Debug.Log(endNode.nodeCoordinates.x.ToString() + " " + endNode.nodeCoordinates.y.ToString());
            Debug.Log("---------");

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            List<GridNode> neighbours = GetNeighbours(currentNode);

            foreach (GridNode neighbour in neighbours)
            {
                if (closedSet.Contains(neighbour))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);

                    neighbour.parentNode = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        // TODO: GET PATH

        return RetracePath(startNode, endNode);
    }

    List<GridNode> RetracePath(GridNode startNode , GridNode endNode)
    {
        List<GridNode> path = new List<GridNode>();

        GridNode node = endNode;

        while (node != startNode)
        {
            path.Add(node);
            node = node.parentNode;
        }

        path.Add(startNode);

        path.Reverse();

        return path;
    }

    List<GridNode> GetNeighbours(GridNode node)
    {
        List<GridNode> neighbours = new List<GridNode>();
        for (int i = 0; i < 5; i++)
        {
            if ((Direction)i == Direction.Up)
            {
                if (node.nodeCoordinates.y < boardSize.y - 1 && grid[node.nodeCoordinates.x, node.nodeCoordinates.y + 1].isFilled && !grid[node.nodeCoordinates.x, node.nodeCoordinates.y + 1].isObstacle)
                {
                    neighbours.Add(grid[node.nodeCoordinates.x + 1, node.nodeCoordinates.y]);
                }
            }
            if ((Direction)i == Direction.Right)
            {
                if (node.nodeCoordinates.x < boardSize.x - 1 && grid[node.nodeCoordinates.x + 1, node.nodeCoordinates.y].isFilled && !grid[node.nodeCoordinates.x + 1, node.nodeCoordinates.y].isObstacle)
                {
                    neighbours.Add(grid[node.nodeCoordinates.x + 1, node.nodeCoordinates.y]);
                }
            }
            if ((Direction)i == Direction.Down)
            {
                if (node.nodeCoordinates.y > 0 && grid[node.nodeCoordinates.x, node.nodeCoordinates.y - 1].isFilled && !grid[node.nodeCoordinates.x, node.nodeCoordinates.y - 1].isObstacle)
                {
                    neighbours.Add(grid[node.nodeCoordinates.x, node.nodeCoordinates.y - 1]);
                }
            }
            if ((Direction)i == Direction.Left)
            {
                if (node.nodeCoordinates.x > 0 && grid[node.nodeCoordinates.x - 1, node.nodeCoordinates.y].isFilled && !grid[node.nodeCoordinates.x - 1, node.nodeCoordinates.y].isObstacle)
                {
                    neighbours.Add(grid[node.nodeCoordinates.x - 1, node.nodeCoordinates.y]);
                }
            }
            if ((Direction)i == Direction.Portal)
            {
                if (node.hasPortal)
                {
                    neighbours.Add(grid[node.portalNode.x, node.portalNode.y]);
                }
            }
        }

        return neighbours;
    }

    int GetDistance(GridNode startNode , GridNode endNode)
    {
        int distance = Mathf.Abs(startNode.nodeCoordinates.x - endNode.nodeCoordinates.x) + Mathf.Abs(startNode.nodeCoordinates.y - endNode.nodeCoordinates.y);
        return distance;
    }

    public void SetGrid(List<TileData> tileData , TwoDCoordinate boardSize)
    {
        this.boardSize = boardSize;
        //grid = new GridNode[boardSize.x, boardSize.y];
        //UpdateGrid(tileData, boardSize);
    }

    public void UpdateGrid(List<TileData> tileData, TwoDCoordinate boardSize , TwoDCoordinate endPos)
    {
        grid = new GridNode[boardSize.x, boardSize.y];
        int index = 0;
        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                GridNode node = new GridNode();

                node.nodeCoordinates.x = x;     //tileData[index].coordinates.x / 2;
                node.nodeCoordinates.y = y;     //tileData[index].coordinates.y / 2;

                node.isVisited = false;

                node.parentNode = null;


                if(tileData[index] != null)
                {
                    node.isFilled = tileData[index].isFilled;
                    node.isObstacle = tileData[index].isObstacle;

                    node.hasPortal = tileData[index].hasPortal;

                    if (node.isFilled)
                    {
                        Debug.Log(node.nodeCoordinates);
                    }
                }
                else
                {
                    node.isFilled = false;
                    node.isObstacle = false;

                    node.hasPortal = false;
                }
                //node.globalGoal = 9999999;
                //node.localGoal = 9999999;

                if (node.hasPortal)
                {
                    node.portalNode = new Vector2Int(tileData[index].portalDestination.x / 2, tileData[index].portalDestination.y / 2);                
                }

                grid[x, y] = node;

                index++;
            }
        }
    }
}