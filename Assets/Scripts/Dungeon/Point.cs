using System;


public class Point
{
    private int x, y;

    public Point(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int X
    {
        get { return this.x; }
    }

    public int Y
    {
        get { return this.y; }
    }
}
