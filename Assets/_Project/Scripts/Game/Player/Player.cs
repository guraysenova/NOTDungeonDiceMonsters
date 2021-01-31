using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ServerPlayer player;

    public ServerPlayer PlayerData
    {
        get { return player; }
    }

    [SerializeField]
    bool wannaSummon;

    [SerializeField]
    Agent selectedAgent;

    [SerializeField]
    int movePoints = 100;

    public static Player instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetPlayerData(TeamEnum teamEnum , PlayerEnum playerEnum , int id)
    {
        player = new ServerPlayer(teamEnum, playerEnum, id);
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

                        if (!BoardManager.instance.Board.GetTile(endPos).isFilled)
                        {
                            selectedAgent = null;
                            return;
                        }

                        List<GridNode> path = BoardManager.instance.Board.PathFinder.GetPath(BoardManager.instance.Board.GetTileData(),startPos, endPos);

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
            if(player.Team == TeamEnum.Team1)
            {
                player.Team = TeamEnum.Team2;
            }
            else if (player.Team == TeamEnum.Team2)
            {
                player.Team = TeamEnum.Team1;
            }
        }
    }

    public void SummonUnit(string id)
    {
        GameObject unit = Instantiate(Units.instance.UnitFromId(id), gameObject.transform.position , Quaternion.Euler(0,0,0));
        
        unit.GetComponent<Agent>().Player = player.Player;
    }

    public void SummonPortal()
    {

    }
}