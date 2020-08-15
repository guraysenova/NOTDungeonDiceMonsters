using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyRoom : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI roomName, playerCountText, gameTypeText;

    string UUID;
    int playerCount = 0;
    GameType gameType = new GameType();

    public void SetData(string roomNameText , int playerAmount , int gameType , string UUID)
    {
        roomName.text = roomNameText;
        playerCountText.text = playerAmount + "/2";
        playerCount = playerAmount;
        gameTypeText.text = ((GameType)gameType).ToString();
        this.gameType = ((GameType)gameType);
        this.UUID = UUID;
    }

    public void JoinRoom()
    {
        ClientSend.LoginRoom(UUID);
    }

    public string RoomUUID
    {
        get
        {
            return UUID;
        }
    }

    public int PlayerCount
    {
        get
        {
            return playerCount;
        }
    }
    public string RoomName
    {
        get
        {
            return roomName.text;
        }
    }

    public int GameTypeIndex
    {
        get
        {
            return (int)gameType;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public bool isReady;

    public PlayerData(string nameVal , bool readyVal)
    {
        playerName = nameVal;
        isReady = readyVal;
    }
}

public enum GameType
{
    Classic = 0
}
