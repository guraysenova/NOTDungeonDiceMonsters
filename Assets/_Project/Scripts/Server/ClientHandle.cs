using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        int myId = packet.ReadInt();

        Debug.Log($"Message from server : {msg}");
        Client.instance.myId = myId;

        ClientSend.WelcomeReceived();
    }

    public static void ConnectedToLobby(Packet packet)
    {
        UIManager.instance.OpenLobbyScreen();
    }

    public static void Token(Packet packet)
    {
        string token = packet.ReadString();
        string ipAdress = packet.ReadString();
        int port = packet.ReadInt();
        SaveManager.Instance.playerState.TokenUUID = packet.ReadString();

        Client.token = token;
        Debug.Log("Token : " + token);
        Debug.Log("IP : " + ipAdress);
        Debug.Log("Port : " + port.ToString());


        Client.instance.SetIpPort(ipAdress , port);
    }

    public static void TokenRequest(Packet packet)
    {
        SaveManager.Instance.playerState.UUID = packet.ReadString();
        Client.instance.myId = packet.ReadInt();

        ClientSend.Token(SaveManager.Instance.playerState.UserName, Client.token);
    }

    public static void LobbyRoom(Packet packet)
    {
        string roomUUID = packet.ReadString();
        string roomName = packet.ReadString();
        int playerCount = packet.ReadInt();
        int gameTypeIndex = packet.ReadInt();

        Lobby.instance.AddRoom(roomUUID, roomName, playerCount, gameTypeIndex);
    }

    public static void RoomData(Packet packet)
    {
        string roomName = packet.ReadString();
        int playerCount  = packet.ReadInt();
        List<PlayerData> players = new List<PlayerData>();
        for (int i = 0; i < playerCount; i++)
        {
            string playerName = packet.ReadString();
            bool isReady = packet.ReadBool();
            players.Add(new PlayerData(playerName, isReady));
        }
        RoomScreen.instance.SetRoomData(roomName, players);
        UIManager.instance.OpenRoomScreen();
    }

    public static void RoomStarting(Packet packet)
    {
        UIManager.instance.OpenStartingRoomScreen();
    }

    public static void RoomStarted(Packet packet)
    {
        Client.roomUUID = packet.ReadString();

        string ipAdress = packet.ReadString();
        int port = packet.ReadInt();

        Client.token = packet.ReadString();

        Client.instance.SetIpPort(ipAdress, port);
    }

    public static void MatchTokenRequest(Packet packet)
    {
        Debug.Log(packet.ReadString());
        Client.instance.myId = packet.ReadInt();

        ClientSend.MatchToken(Client.token , Client.instance.myId, Client.roomUUID , SaveManager.Instance.playerState.UUID);
    }

    public static void MatchStarted(Packet packet)
    {
        TeamEnum teamEnum = (TeamEnum)packet.ReadInt();
        PlayerEnum playerEnum = (PlayerEnum)packet.ReadInt();
        Player.instance.SetPlayerData(teamEnum, playerEnum);
        // TODO: Get is player number one or two , set game up , open game UI etc
    }

    public static void PlaceBox(Packet packet)
    {
        
    }

    public static void MoveAgent(Packet packet)
    {

    }

    public static void Attack(Packet packet)
    {

    }

    public static void EndTurn(Packet packet)
    {

    }
}
