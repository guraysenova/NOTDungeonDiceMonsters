using UnityEngine;

public class GridNode
{
    public bool isFilled;
    public bool isObstacle;

    public bool isVisited;

    public bool hasPortal;

    public Vector2Int nodeCoordinates;

    public Vector2Int portalNode;
    public Vector2Int portalNodeCoordinates;

    public int globalGoal;
    public int localGoal;

    public GridNode parentNode;
}