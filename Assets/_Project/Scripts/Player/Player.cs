﻿using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerEnum player = PlayerEnum.Player1;

    [SerializeField]
    TeamEnum team = TeamEnum.Team1;

    [SerializeField]
    bool wannaSummon;

    [SerializeField]
    Agent selectedAgent;

    [SerializeField]
    int movePoints = 100;


    public PlayerEnum PlayerVal
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }

    public TeamEnum TeamVal
    {
        get
        {
            return team;
        }
        set
        {
            team = value;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Unit"))
                {
                    selectedAgent = hit.transform.GetComponent<Agent>();
                }
                else
                {
                    selectedAgent = null;
                }
            }
            else
            {
                selectedAgent = null;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Board"))
                {
                    if(selectedAgent != null && movePoints > 0)
                    {
                        TwoDCoordinate startPos = new TwoDCoordinate(Mathf.RoundToInt(selectedAgent.gameObject.transform.position.x) , Mathf.RoundToInt(selectedAgent.gameObject.transform.position.z));

                        TwoDCoordinate endPos = new TwoDCoordinate(Mathf.RoundToInt(gameObject.transform.position.x) , Mathf.RoundToInt(gameObject.transform.position.z));

                        if (!BoardManager.instance.GetTile(endPos).isFilled)
                        {
                            selectedAgent = null;
                            return;
                        }

                        List<GridNode> path = BoardManager.instance.PathFinder.GetPath(startPos, endPos);

                        // move
                        selectedAgent.GoPath(path, movePoints);

                        if (path.Count >= movePoints)
                        {
                            movePoints = 0;
                        }
                        else
                        {
                            movePoints -= path.Count;
                        }

                        selectedAgent = null;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            wannaSummon = !wannaSummon;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if(team == TeamEnum.Team1)
            {
                team = TeamEnum.Team2;
            }
            else if (team == TeamEnum.Team2)
            {
                team = TeamEnum.Team1;
            }
        }
    }

    public void SummonUnit(string id)
    {
        GameObject unit = Instantiate(Units.instance.UnitFromId(id), gameObject.transform.position , Quaternion.Euler(0,0,0));
        
        unit.GetComponent<Agent>().Player = player;
    }

    public void SummonPortal()
    {

    }
}