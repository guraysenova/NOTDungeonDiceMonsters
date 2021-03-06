using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client instance;

    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 26950;
    public int myId = 0;
    public TCP tcp;

    public static string token = "";

    public static string roomUUID = "";

    private bool isConnected = false;

    private delegate void PacketHandler(Packet packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    internal void SetIpPort(string ipAdress, int portVal)
    {
        ip = ipAdress;
        port = portVal;
        //Disconnect();
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        InitializeClientData();

        isConnected = true;

        tcp = new TCP();

        tcp.Connect();
    }

    public class TCP
    {
        public TcpClient socket;

        private Packet receivedData;
        private NetworkStream stream;
        private byte[] receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            socket.EndConnect(result);

            if (!socket.Connected)
            {
                return;
            }
            stream = socket.GetStream();

            receivedData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        public void SendData(Packet packet)
        {
            try
            {
                if(socket != null)
                {
                    stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                }
            }
            catch(Exception exception)
            {
                Debug.Log($"Error sending data to server via TCP: {exception}");
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = stream.EndRead(result);
                if (byteLength <= 0)
                {
                    instance.Disconnect();
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(receiveBuffer, data, byteLength);

                receivedData.Reset(HandleData(data));

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
                Disconnect();

            }
        }

        private void Disconnect()
        {
            instance.Disconnect();

            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;
        }

        private bool HandleData(byte[] data)
        {
            int packetLength = 0;
            receivedData.SetBytes(data);

            if(receivedData.UnreadLength() >= 4)
            {
                packetLength = receivedData.ReadInt();
                if(packetLength <= 0)
                {
                    return true;
                }
            }

            while(packetLength > 0 && packetLength <= receivedData.UnreadLength())
            {
                byte[] packetBytes = receivedData.ReadBytes(packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        packetHandlers[packetId](packet);
                    }
                });

                packetLength = 0;

                if (receivedData.UnreadLength() >= 4)
                {
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }
            }

            if(packetLength <= 1)
            {
                return true;
            }
            return false;
        }
    
    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int)ServerPackets.Welcome , ClientHandle.Welcome },
            {(int)ServerPackets.Token , ClientHandle.Token },
            {(int)ServerPackets.TokenRequest , ClientHandle.TokenRequest },
            {(int)ServerPackets.LobbyRoom , ClientHandle.LobbyRoom },
            {(int)ServerPackets.ConnectedToLobby , ClientHandle.ConnectedToLobby },
            {(int)ServerPackets.RoomData , ClientHandle.RoomData },
            {(int)ServerPackets.RoomStarting , ClientHandle.RoomStarting },
            {(int)ServerPackets.RoomStarted , ClientHandle.RoomStarted },
            {(int)ServerPackets.MatchTokenRequest , ClientHandle.MatchTokenRequest },
            {(int)ServerPackets.MatchStarted , ClientHandle.MatchStarted },
            {(int)ServerPackets.PlaceBox , ClientHandle.PlaceBox },
            {(int)ServerPackets.MoveAgent , ClientHandle.MoveAgent },
            {(int)ServerPackets.Attack , ClientHandle.Attack },
            {(int)ServerPackets.EndTurn , ClientHandle.EndTurn }
        };
        //Debug.Log("Initialized packets");
    }

    private void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;
            tcp.socket.Client.Disconnect(false);
            //tcp.socket.GetStream().Close();
            //tcp.socket.Close();
            //tcp.socket.Client.Shutdown(SocketShutdown.Both);

            Debug.Log("Disconnected");
        }
    }

    private void OnApplicationQuit()
    {
        //tcp.socket.Client.Shutdown(SocketShutdown.Both);
        //tcp.socket.GetStream().Close();
        //tcp.socket.Close();
        Disconnect();
    }
}
