using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class DirectionHelperTest
{

    [Test]
    public void ShouldReturnCoordForTheDirection()
    {
        Assert.AreEqual(new Coord(0, 1), Direction.Up.ToCoord());
        Assert.AreEqual(new Coord(0, -1), Direction.Down.ToCoord());
        Assert.AreEqual(new Coord(1, 0), Direction.Right.ToCoord());
        Assert.AreEqual(new Coord(-1, 0), Direction.Left.ToCoord());
    }

    [Test]
    public void ShouldReturnOppositeDirection()
    {
        Assert.AreEqual(Direction.Down, Direction.Up.GetOpposite());
        Assert.AreEqual(Direction.Up, Direction.Down.GetOpposite());
        Assert.AreEqual(Direction.Left, Direction.Right.GetOpposite());
        Assert.AreEqual(Direction.Right, Direction.Left.GetOpposite());
    }

}
