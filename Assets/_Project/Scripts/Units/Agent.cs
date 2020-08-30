using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    PlayerEnum player;

    public PlayerEnum Player
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }

    public void GoPath(List<GridNode> path, int movePoints)
    {
        List<Vector3> newPath = new List<Vector3>();

        for (int i = 0; i < movePoints; i++)
        {
            if(i == path.Count)
            {
                break;
            }
            newPath.Add(new Vector3(path[i].nodeCoordinates.x * 2 , 0 , path[i].nodeCoordinates.y * 2));
        }


        // TODO: IMPLEMENT PORTAL CHECK.

        gameObject.GetComponent<MovementAgent>().GoPath(newPath);
    }
}