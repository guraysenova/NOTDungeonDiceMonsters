using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    TurnManager turnManager;

    UIManager uiManager;

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    public void GameStarted(List<Team> teams)
    {
        turnManager = new TurnManager(teams);
        SetTurn();
        uiManager.OpenGameScreen();
    }

    // add things that trigger turn/phase change

    void SetTurn()
    {
        Turn turn = turnManager.Turn;
        ServerPlayer player = Player.instance.PlayerData;

        string phaseName;
        if (turn.Phase == TurnPhase.Battle)
        {
            phaseName = "BATTLE PHASE";
        }
        else if(turn.Phase == TurnPhase.Roll)
        {
            phaseName = "ROLL PHASE";
        }
        else
        {
            phaseName = "SUMMON PHASE";
        }

        string turnName;
        if (turn.TeamIndex == (int)player.Team)
        {
            turnName = "ALLY TURN";
            if (turn.PlayerIndexInTeam == (int)player.Player)
            {
                turnName = "YOUR TURN";
            }
        }
        else
        {
            turnName = "ENEMY TURN";
        }

        UIManager.instance.SetTurnData(turnName, phaseName);
    }
}
