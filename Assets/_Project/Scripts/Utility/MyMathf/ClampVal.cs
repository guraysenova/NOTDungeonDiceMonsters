using UnityEngine;

[System.Serializable]
public class ClampVal
{
    public ClampVal()
    {
        minVal = -9999999f;
        maxVal = 9999999f;
        clampAroundAngle = 0;
    }

    [SerializeField]
    float minVal, maxVal , clampAroundAngle;

    public float Min
    {
        get
        {
            return minVal;
        }
        set
        {
            minVal = value;
        }
    }
    public float Max
    {
        get
        {
            return maxVal;
        }
        set
        {
            maxVal = value;
        }
    }
    public float ClampAround
    {
        get
        {
            return clampAroundAngle;
        }
        set
        {
            clampAroundAngle = value;
        }
    }
}