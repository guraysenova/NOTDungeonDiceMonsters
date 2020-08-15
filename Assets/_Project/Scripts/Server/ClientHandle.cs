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

    public static void ConnectedToMatch(Packet packet)
    {
        // open game screen set up
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
}
