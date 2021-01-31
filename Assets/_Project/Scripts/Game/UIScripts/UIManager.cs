using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    Animator animator;

    [SerializeField]
    TextMeshProUGUI turnText, phaseText;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        OpenLoginScreen();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OpenLoginScreen()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
        animator.SetLayerWeight(4, 0);
        animator.SetLayerWeight(5, 0);

        animator.SetTrigger("EP_LoginScreen");
    }

    public void OpenWaitingScreen()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
        animator.SetLayerWeight(4, 0);
        animator.SetLayerWeight(5, 0);

        animator.SetTrigger("EP_WaitingConnection");
    }

    public void OpenLobbyScreen()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 1);
        animator.SetLayerWeight(3, 0);
        animator.SetLayerWeight(4, 0);
        animator.SetLayerWeight(5, 0);

        animator.SetTrigger("L_LobbyScreen");
    }

    public void OpenCreateRoomScreen()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 1);
        animator.SetLayerWeight(4, 0);
        animator.SetLayerWeight(5, 0);

        animator.SetTrigger("CR_CreateRoomScreen");
    }

    public void OpenRoomScreen()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
        animator.SetLayerWeight(4, 1);
        animator.SetLayerWeight(5, 0);

        animator.SetTrigger("R_RoomScreen");
    }

    public void OpenStartingRoomScreen()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
        animator.SetLayerWeight(4, 1);
        animator.SetLayerWeight(5, 0);

        animator.SetTrigger("R_StartingRoom");
    }

    public void OpenGameScreen()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
        animator.SetLayerWeight(4, 0);
        animator.SetLayerWeight(5, 1);

        animator.SetTrigger("G_GameScreen");
    }

    public void SetGameUIData()
    {

    }

    public void SetTurnData(string turnType , string phaseType)
    {
        turnText.text = turnType;
        phaseText.text = phaseType;
    }
}
