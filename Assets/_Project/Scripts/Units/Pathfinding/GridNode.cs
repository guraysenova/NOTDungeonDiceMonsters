using UnityEngine;

public class GridNode
{
    public bool isFilled;
    public bool isObstacle;

    public bool isVisited;

    public bool hasPortal;

    public Vector2Int nodeCoordinates;

    public Vector2Int portalNode;

    public int gCost; // G Cost
    public int hCost; // H Cost

    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public GridNode parentNode;
}