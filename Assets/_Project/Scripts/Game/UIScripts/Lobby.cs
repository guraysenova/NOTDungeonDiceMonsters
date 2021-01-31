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
        foreach (var val in rooms)
        {
            Destroy(val.gameObject);
        }
        rooms.Clear();

        ClientSend.LobbyRoomRequest();
    }

    public void AddRoom(string roomUUID , string roomName, int playerCount , int gameTypeIndex)
    {
        GameObject room = Instantiate(UIRoomPrefab , content.transform);
        room.GetComponent<LobbyRoom>().SetData(roomName, playerCount, gameTypeIndex, roomUUID);
        rooms.Add(room.GetComponent<LobbyRoom>());
    }
}
