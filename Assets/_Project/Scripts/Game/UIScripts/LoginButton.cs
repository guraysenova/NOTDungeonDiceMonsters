using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameInputField;

    public void Login()
    {
        if(nameInputField.text.Length < 1)
        {
            return;
        }
        else
        {
            SaveManager.Instance.playerState.UserName = nameInputField.text;
            Client.instance.ConnectToServer();
            UIManager.instance.OpenWaitingScreen();
        }
    }
}
