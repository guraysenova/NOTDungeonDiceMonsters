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

    public static void LoginRoom(string roomUUID)
    {
        using (Packet packet = new Packet((int)ClientPackets.LoginRoom))
        {
            packet.Write(roomUUID);

            SendTCPData(packet);
        }
    }

    public static void ToggleReady()
    {
        using (Packet packet = new Packet((int)ClientPackets.ToggleReady))
        {
            SendTCPData(packet);
        }
    }

    public static void ExitRoom()
    {
        using (Packet packet = new Packet((int)ClientPackets.ExitRoom))
        {
            SendTCPData(packet);
        }
    }

    public static void StartRoom()
    {
        using (Packet packet = new Packet((int)ClientPackets.StartRoom))
        {
            SendTCPData(packet);
        }
    }

    public static void MatchToken(string token , int id ,string roomUUID , string clientUUID)
    {
        using(Packet packet = new Packet((int)ClientPackets.MatchToken))
        {
            packet.Write(token);
            packet.Write(id);
            packet.Write(roomUUID);
            packet.Write(clientUUID);

            SendTCPData(packet);
        }
    }

    public static void Ready()
    {
        using (Packet packet = new Packet((int)ClientPackets.Ready))
        {


            SendTCPData(packet);
        }
    }

    public static void PlaceBox()
    {
        using (Packet packet = new Packet((int)ClientPackets.PlaceBox))
        {


            SendTCPData(packet);
        }
    }

    public static void MoveAgent()
    {
        using (Packet packet = new Packet((int)ClientPackets.MoveAgent))
        {


            SendTCPData(packet);
        }
    }

    public static void Attack()
    {
        using (Packet packet = new Packet((int)ClientPackets.Attack))
        {


            SendTCPData(packet);
        }
    }

    public static void EndTurn()
    {
        using (Packet packet = new Packet((int)ClientPackets.EndTurn))
        {


            SendTCPData(packet);
        }
    }

    // TODO: Plan out game data etc and make functions here
}
