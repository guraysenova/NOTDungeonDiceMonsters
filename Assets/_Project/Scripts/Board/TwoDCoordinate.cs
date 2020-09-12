[System.Serializable]
public class TwoDCoordinate
{
    public TwoDCoordinate(int _x , int _y)
    {
        x = _x;
        y = _y;
    }

    public int x;
    public int y;

    public static int Distance(TwoDCoordinate first , TwoDCoordinate second)
    {
        int xDiff = first.x - second.x;
        int yDiff = first.y - second.y;

        if(xDiff < 0)
        {
            xDiff *= -1;
        }

        if (yDiff < 0)
        {
            yDiff *= -1;
        }

        return xDiff + yDiff;
    }
}