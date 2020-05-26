using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTeam : MonoBehaviour
{
    public static BoxTeam instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [SerializeField]
    List<BoxTeamData> teamDatas = new List<BoxTeamData>();

    public BoxTeamData GetTeamData(TeamEnum team)
    {
        BoxTeamData val = null;
        foreach (BoxTeamData teamData in teamDatas)
        {
            if(teamData.team == team)
            {
                val = teamData;
            }
        }
        return val;
    }

    public Material[] GetMaterialArray(TeamEnum team)
    {
        BoxTeamData val = null;
        foreach (BoxTeamData teamData in teamDatas)
        {
            if (teamData.team == team)
            {
                val = teamData;
            }
        }

        Material[] materials = { val.bottomOuterColor, val.topColorTwo , val.topColorOne , val.bottomColor , val.crestColor};
        return materials;
    }
}
