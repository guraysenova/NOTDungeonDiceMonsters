using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public TwoDCoordinate coordinates;

    public bool isFilled;

    public bool isConnected;

    public List<TeamEnum> teams = new List<TeamEnum>();

    public bool hasPortal;

    public int portalX;
    public int portalY;

    public void AddTeam(TeamEnum team)
    {
        if (!DoesTeamExist(team))
        {
            teams.Add(team);
        }
    }

    public bool DoesTeamExist(TeamEnum teamVal)
    {
        return teams.Contains(teamVal);
    }
}
