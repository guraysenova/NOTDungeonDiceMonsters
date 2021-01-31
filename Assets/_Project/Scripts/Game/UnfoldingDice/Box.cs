using UnityEngine;

public class Box : MonoBehaviour
{
    public void SetTeam(TeamEnum team)
    {
        Material[] materials = BoxTeam.instance.GetMaterialArray(team);
        Debug.Log(gameObject.name);
        foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (item.enabled)
            {
                item.materials = materials;
                /*
                item.materials.SetValue(BoxTeam.instance.GetTeamData(team).bottomOuterColor, 0);
                item.materials.SetValue(BoxTeam.instance.GetTeamData(team).topColorTwo, 1);
                item.materials.SetValue(BoxTeam.instance.GetTeamData(team).topColorOne, 2);
                item.materials.SetValue(BoxTeam.instance.GetTeamData(team).bottomColor, 3);
                item.materials.SetValue(BoxTeam.instance.GetTeamData(team).crestColor, 4);
                */
            }
        }
    }
}