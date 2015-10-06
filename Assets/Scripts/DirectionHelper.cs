using UnityEngine;
using System.Collections;

public static class DirectionHelper 
{

    public static Coord ToCoord(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Coord(0, 1);
            case Direction.Down:
                return new Coord(0, -1);
            case Direction.Left:
                return new Coord(-1, 0);
            case Direction.Right:
                return new Coord(1, 0);
            default:
                return new Coord(0, 0);
        }
    }

    public static Direction GetOpposite(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return Direction.Down;
        }
    }

}
