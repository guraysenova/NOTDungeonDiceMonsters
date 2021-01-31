using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TurnManager
{
    List<Team> teams;

    int turnCount;

    int playerCount;

    public Turn Turn { get; }

    public TurnManager(List<Team> teams)
    {
        turnCount = 0;
        this.teams = teams;
        Turn = new Turn();

        foreach (var team in teams)
        {
            playerCount += team.Players.Count;
        }
    }

    bool IsPlayersTurn(int playerID)
    {
        int teamIndex = 0;
        int playerIndex = 0;

        bool playerFound = false;

        for (int i = 0; i < teams.Count; i++)
        {
            for (int j = 0; j < teams[i].Players.Count; j++)
            {
                if (teams[i].Players[j].ID == playerID)
                {
                    teamIndex = i;
                    playerIndex = j;
                    playerFound = true;
                }
            }
            if (playerFound)
            {
                break;
            }
        }
        if (!playerFound)
        {
            Debug.Log("Player Not Found with given ID : " + playerID);
            return false;
        }

        if (Turn.PlayerIndexInTeam == playerIndex && Turn.TeamIndex == teamIndex)
        {
            return true;
        }

        return false;
    }

    bool IsPhase(TurnPhase phase)
    {
        if (Turn.Phase == phase)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayersTurnAndPhase(int playerID, TurnPhase phase)
    {
        return IsPlayersTurn(playerID) && IsPhase(phase);
    }

    public void AdvancePhase()
    {
        if (Turn.Phase == TurnPhase.Battle)
        {
            turnCount++;
            Turn.TeamIndex = (Turn.TeamIndex + 1) % teams.Count;
            Turn.Phase = TurnPhase.Roll;
            Turn.PlayerIndexInTeam = (turnCount % playerCount) / teams.Count;
        }
        else
        {
            Turn.Phase = (TurnPhase)((int)Turn.Phase + 1);
        }
    }
}
