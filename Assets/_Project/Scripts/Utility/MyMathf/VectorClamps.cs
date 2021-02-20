using UnityEngine;

[System.Serializable]
public class VectorClamps
{
    public VectorClamps()
    {
        xClamp = new ClampVal();
        yClamp = new ClampVal();
        zClamp = new ClampVal();
    }

    [SerializeField]
    ClampVal xClamp  = new ClampVal(), yClamp = new ClampVal(), zClamp = new ClampVal();

    [SerializeField]
    public ClampVal x
    {
        get
        {
            if (xClamp == null)
            {
                xClamp = new ClampVal();
            }
            return xClamp;
        }
        set
        {
            if (xClamp == null)
            {
                xClamp = new ClampVal();
            }
            xClamp = value;
        }
    }
    public ClampVal y
    {
        get
        {
            if (yClamp == null)
            {
                yClamp = new ClampVal();
            }
            return yClamp;
        }
        set
        {
            if (yClamp == null)
            {
                yClamp = new ClampVal();
            }
            yClamp = value;
        }
    }
    public ClampVal z
    {
        get
        {
            if (zClamp == null)
            {
                zClamp = new ClampVal();
            }
            return zClamp;
        }
        set
        {
            if (zClamp == null)
            {
                zClamp = new ClampVal();
            }
            zClamp = value;
        }
    }
}
