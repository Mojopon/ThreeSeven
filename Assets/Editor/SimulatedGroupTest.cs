using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class SimulatedGroupTest : GridTestFixture
{
    ISimulatedGroup simulatedGroup;

    [SetUp]
    public void CreateSimulatedGroup()
    {
        simulatedGroup = new SimulatedGroup();
        group.SetLocation(setting.BlockSpawnPoint);
    }

    [Test]
    public void ShouldSimulateFromTheGroup()
    {
        Assert.AreEqual(group.Location, setting.BlockSpawnPoint);

        simulatedGroup.Simulate(group);

        Assert.AreEqual(group.Location, simulatedGroup.Location);
        Assert.AreEqual(group.GetPattern(), simulatedGroup.GetPattern());
        Assert.AreEqual(group.ChildrenLocation.Length, simulatedGroup.ChildrenLocation.Length);

        for(int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(group.ChildrenLocation[i], simulatedGroup.ChildrenLocation[i]);
            Assert.AreEqual(group.Children[i].Number, simulatedGroup.Children[i].Number);
            Assert.AreEqual(group.Children[i].BlockType, simulatedGroup.Children[i].BlockType);
        }
    }

    [Test]
    public void ShouldMoveSimulatedGroup()
    {
        simulatedGroup.Simulate(group);

        group.Move(Direction.Right);
        simulatedGroup.Move(Direction.Right);
        Assert.AreEqual(group.Location, simulatedGroup.Location);

        for (int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(group.ChildrenLocation[i], simulatedGroup.ChildrenLocation[i]);
        }
    }

    [Test]
    public void ShouldSetLocation()
    {
        Coord destination = setting.BlockSpawnPoint + Direction.Right.ToCoord();

        simulatedGroup.Simulate(group);

        group.SetLocation(destination);
        simulatedGroup.SetLocation(destination);

        Assert.AreEqual(group.Location, simulatedGroup.Location);

        for (int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(group.ChildrenLocation[i], simulatedGroup.ChildrenLocation[i]);
        }
    }

    [Test]
    public void ShouldRotate()
    {
        simulatedGroup.Simulate(group);

        group.Rotate(RotateDirection.Clockwise);
        simulatedGroup.Rotate(RotateDirection.Clockwise);

        Assert.AreEqual(group.Location, simulatedGroup.Location);

        for (int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(group.ChildrenLocation[i], simulatedGroup.ChildrenLocation[i]);
        }
    }
}
