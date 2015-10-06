using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Coord {

    public int X;
    public int Y;

    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(Mathf.RoundToInt(X), Mathf.RoundToInt(Y));
    }

    public static Coord operator +(Coord lhs, Coord rhs)
    {
        return new Coord(lhs.X + rhs.X, lhs.Y + rhs.Y);
    }

    public static Coord operator -(Coord lhs, Coord rhs)
    {
        return new Coord(lhs.X - rhs.X, lhs.Y - rhs.Y);
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", X, Y);
    }
}
