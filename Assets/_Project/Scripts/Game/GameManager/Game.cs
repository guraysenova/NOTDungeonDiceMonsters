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
    public void GameStarted(List<Team> teams)
    {
        turnManager = new TurnManager(teams);
    }
}
