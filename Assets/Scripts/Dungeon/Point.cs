﻿using System;

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

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    public static Point operator +(Point pt1, Point pt2)
    {
        return new Point(pt1.X + pt2.X, pt1.Y + pt2.Y);
    }

    public override int GetHashCode()
    {
        int hash = 3;
        // They have to return the same value to even check Equals()
        return x * hash + y * 2 * hash;
    }

    public override bool Equals(Object obj)
    {
        return Equals(obj as Point);
    }

    public bool Equals(Point obj)
    {
        // If the X and Y values are the same, they are the same object
        return obj != null && obj.X == this.X && obj.Y == this.Y;
    }
}
