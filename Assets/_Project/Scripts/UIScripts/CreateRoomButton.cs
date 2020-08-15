using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateRoomButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI roomName;

    public void CreateRoom()
    {
        if(roomName.text.Length < 0)
        {
            return;
        }
        else
        {
            ClientSend.CreateRoom(roomName.text);
            UIManager.instance.OpenRoomScreen();
        }
    }
}
