using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class CoordTest {

	[Test]
    public void ShouldAdd()
    {
        Coord coordOne = new Coord(2, 1);
        Coord coordTwo = new Coord(3, 1);
        Coord coordAdded = coordOne + coordTwo;
        Assert.IsTrue(coordAdded.X == 5);
        Assert.IsTrue(coordAdded.Y == 2);
    }

    [Test]
    public void ShouldSubtract()
    {
        Coord coordOne = new Coord(4, 5);
        Coord coordTwo = new Coord(7, 2);
        Coord coordAdded = coordOne - coordTwo;
        Assert.IsTrue(coordAdded.X == -3);
        Assert.IsTrue(coordAdded.Y == 3);
    }

    [Test]
    public void ShouldBeEqual()
    {
        Coord coordOne = new Coord(2, 2);
        Coord CoordTwo = new Coord(2, 2);
        Assert.IsTrue(coordOne.Equals(CoordTwo));
    }

    [Test]
    public void ShouldConvertToVector3()
    {
        Coord coord = new Coord(2, 2);
        Vector2 vector = coord.ToVector2();
        Assert.AreEqual(2f, vector.x, 0.001f);
        Assert.AreEqual(2f, vector.y, 0.001f);
    }
}
