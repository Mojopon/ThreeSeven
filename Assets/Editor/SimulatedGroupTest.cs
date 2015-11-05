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
    }

    [Test]
    public void ShouldSimulateFromTheGroup()
    {
        Coord spawnPoint = setting.BlockSpawnPoint;
        group.SetLocation(spawnPoint);
        Assert.AreEqual(group.Location, spawnPoint);

        simulatedGroup.Simulate(group);
        foreach(IBlock block in group.Children)
        {
            Debug.Log(block.Location);
        }

        Assert.AreEqual(group.Location, simulatedGroup.Location);
        Assert.AreEqual(group.GetPattern(), simulatedGroup.GetPattern());
        Assert.AreEqual(group.ChildrenLocation.Length, simulatedGroup.ChildrenLocation.Length);

        for(int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(group.ChildrenLocation[i], simulatedGroup.ChildrenLocation[i]);
        }
    }
}
