using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    List<CameraPosition> cameraPositions = new List<CameraPosition>();
    public static CameraController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetPosition(TeamEnum team , PlayerEnum player)
    {
        foreach (var cameraPosition in cameraPositions)
        {
            if(cameraPosition.PlayerEnum == player && cameraPosition.TeamEnum == team)
            {
                transform.position = cameraPosition.Position;
                transform.rotation = Quaternion.Euler(cameraPosition.Rotation);
            }
        }
    }
}
