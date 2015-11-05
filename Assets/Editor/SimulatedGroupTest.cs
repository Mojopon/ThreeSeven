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
        }
    }

    [Test]
    public void ShouldMoveSimulatedGroup()
    {
        simulatedGroup.Move(Direction.Right);
        Assert.AreEqual(group.Location + Direction.Right.ToCoord(), simulatedGroup.Location);
    }
}
