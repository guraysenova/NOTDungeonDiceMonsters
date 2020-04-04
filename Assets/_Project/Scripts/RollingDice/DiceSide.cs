using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField]
    CrestType crestType = new CrestType();

    [SerializeField , Range(1,4 , order = 0)]
    int crestTier;

    [SerializeField]
    Color highlightColor = new Color();
    public void Highlight()
    {
        foreach (Transform item in gameObject.transform)
        {
            item.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", highlightColor);
        }
    }

    public void UnHighLight()
    {
        foreach (Transform item in gameObject.transform)
        {
            item.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
        }
    }
}