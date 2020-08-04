using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public static Lobby instance;

    List<LobbyRoom> rooms = new List<LobbyRoom>();

    [SerializeField]
    GameObject content;

    [SerializeField]
    GameObject UIRoomPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Refresh()
    {
        rooms.Clear();
    }

    public void AddRoom(string roomUUID , string )
    {

    }
}
