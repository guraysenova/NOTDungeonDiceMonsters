using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomScreen : MonoBehaviour
{
    [SerializeField]
    GameObject nameGO;
    [SerializeField]
    List<GameObject> playerNameGOs = new List<GameObject>();
    [SerializeField]
    List<GameObject> playerReadyGOs = new List<GameObject>();

    Color readyColor = Color.green;

    Color nonReadyColor = Color.red;

    public static RoomScreen instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetRoomData(string roomName , List<PlayerData> players)
    {
        nameGO.GetComponent<TextMeshProUGUI>().text = roomName;

        foreach (var playerNameGO in playerNameGOs)
        {
            playerNameGO.GetComponent<TextMeshProUGUI>().text = "Empty";
        }

        foreach (var playerReadyGO in playerReadyGOs)
        {
            playerReadyGO.GetComponent<Image>().color = nonReadyColor;
        }

        for (int i = 0; i < players.Count; i++)
        {
            playerNameGOs[i].GetComponent<TextMeshProUGUI>().text = players[i].playerName;
            if (players[i].isReady)
            {
                playerReadyGOs[i].GetComponent<Image>().color = readyColor;
            }
        }
    }

    public void ToggleReady()
    {
        ClientSend.ToggleReady();
    }
}
