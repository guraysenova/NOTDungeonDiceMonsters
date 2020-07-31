using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyRoom : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI roomName, playerCount, gameType;

    string UUID;

    public void SetData(string roomNameText , int playerAmount , string gameTypeText , string UUID)
    {
        roomName.text = roomNameText;
        playerCount.text = playerAmount + "/2";
        gameType.text = gameTypeText;
        this.UUID = UUID;
    }

    public void JoinRoom()
    {

    }
}
