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
            packet.Write("Guray");

            SendTCPData(packet);
        }
    }

    public static void Token(string username , string token)
    {
        using (Packet packet = new Packet((int)ClientPackets.Token))
        {
            packet.Write(Client.instance.myId);
            packet.Write("Guray");
            packet.Write(token);

            SendTCPData(packet);
        }
    }
}
