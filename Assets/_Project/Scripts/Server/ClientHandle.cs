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

    public static void Token(Packet packet)
    {
        string token = packet.ReadString();
        string ipAdress = packet.ReadString();
        int port = packet.ReadInt();

        Debug.Log("Token : " + token);
        Debug.Log("IP : " + ipAdress);
        Debug.Log("Port : " + port.ToString());

        Client.instance.SetIpPort(ipAdress , port);
    }
}
