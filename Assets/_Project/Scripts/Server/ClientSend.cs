using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        Client.instance.tcp.SendData(packet);
    }

    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.WelcomeReceived))
        {
            packet.Write(Client.instance.myId);
            packet.Write(SaveManager.Instance.playerState.UserName);

            SendTCPData(packet);
        }
    }

    public static void Token(string username , string token)
    {
        using (Packet packet = new Packet((int)ClientPackets.Token))
        {
            packet.Write(Client.instance.myId);
            packet.Write(username);
            packet.Write(SaveManager.Instance.playerState.TokenUUID);
            packet.Write(token);

            SendTCPData(packet);
        }
    }

    public static void LobbyRoomRequest()
    {
        using (Packet packet = new Packet((int)ClientPackets.LobbyRoomRequest))
        {
            SendTCPData(packet);
        }
    }

    public static void CreateRoom(string roomName)
    {
        using (Packet packet = new Packet((int)ClientPackets.CreateRoom))
        {
            packet.Write(SaveManager.Instance.playerState.UUID);
            packet.Write(roomName);

            SendTCPData(packet);
        }
    }
}
