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

        grid[startPos.x / 2, startPos.y / 2].globalGoal = Mathf.Abs((startPos.x / 2) - (endPos.x / 2)) + Mathf.Abs((startPos.y / 2) - (endPos.y / 2));
        grid[startPos.x / 2, startPos.y / 2].localGoal = 0;

        // TODO: GET PATH

        return path;
    }

    public void SetGrid(List<TileData> tileData , TwoDCoordinate boardSize)
    {
        this.boardSize = boardSize;
        grid = new GridNode[boardSize.x, boardSize.y];
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

                node.isFilled = tileData[index].isFilled;
                node.isObstacle = tileData[index].isObstacle;

                node.hasPortal = tileData[index].hasPortal;

                node.globalGoal = 9999999;
                node.localGoal = 9999999;

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