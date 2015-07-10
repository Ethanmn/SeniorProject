using System;

public class PointF
{
    private float x, y;

    public PointF(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

    public float X
    {
        get { return this.x; }
    }

    public float Y
    {
        get { return this.y; }
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    public static PointF operator +(PointF pt1, PointF pt2)
    {
        return new PointF(pt1.X + pt2.X, pt1.Y + pt2.Y);
    }

    public override int GetHashCode()
    {
        int hash = 3;
        // They have to return the same value to even check Equals()
        return (int)(x * hash + y * 2 * hash);
    }

    public override bool Equals(Object obj)
    {
        return Equals(obj as PointF);
    }

    public bool Equals(PointF obj)
    {
        // If the X and Y values are the same, they are the same object
        return obj != null && obj.X == this.X && obj.Y == this.Y;
    }
}
