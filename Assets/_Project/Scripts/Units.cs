using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    [SerializeField]
    List<UnitData> units = new List<UnitData>();

    public static Units instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject UnitFromId(string id)
    {
        foreach (UnitData unit in units)
        {
            if(unit.id == id)
            {
                return unit.prefab;
            }
        }
        return null;
    }
}