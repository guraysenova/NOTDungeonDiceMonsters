using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField]
    CrestType crestType = new CrestType();

    Color mainColor = new Color();

    [SerializeField , Range(1,4 , order = 0)]
    int crestTier;

    private void Start()
    {
        mainColor = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.GetColor("_Color");
    }

    public void Highlight(Color highlightColor)
    {
        foreach (Transform item in gameObject.transform)
        {
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", highlightColor);
        }
    }

    public void UnHighLight()
    {
        foreach (Transform item in gameObject.transform)
        {
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", mainColor);
        }
    }
}