using UnityEngine;
[System.Serializable]
public class CameraPosition
{
    [SerializeField]
    TeamEnum teamEnum;
    [SerializeField]
    PlayerEnum playerEnum;
    [SerializeField]
    Vector3 cameraPosition;
    [SerializeField]
    Vector3 cameraRotation;

    public TeamEnum TeamEnum
    {
        get { return teamEnum; }
    }
    public PlayerEnum PlayerEnum
    {
        get { return playerEnum; }
    }
    public Vector3 Position
    {
        get { return cameraPosition; }
    }
    public Vector3 Rotation
    {
        get { return cameraRotation; }
    }
}