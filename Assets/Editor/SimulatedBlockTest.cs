using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class SimulatedBlockTest
{
    ISimulatedBlock simulatedBlock;

    [Test]
    public void CreateSimulatedBlock()
    {
        simulatedBlock = new SimulatedBlock();
    }

    [Test]
    public void ShouldHasDefaultValueWhenCreatedWithNoConstructor()
    {
        Assert.AreEqual(-1, simulatedBlock.Number);
        Assert.AreEqual(BlockType.None, simulatedBlock.BlockType);

        Assert.AreEqual(new Coord(0, 0), simulatedBlock.Location);
        Assert.AreEqual(new Coord(0, 0), simulatedBlock.LocationInTheGroup);
    }

    [Test]
    public void LocationShouldBeAffectedByLocationInTheGroup()
    {
        var block = new SimulatedBlock();
        block.LocationInTheGroup = new Coord(0, 2);
        block.SetLocation(new Coord(1, 2));
        Assert.AreEqual(new Coord(1, 4), block.Location);
    }

    [Test]
    public void ShouldIgnoreLocationInTheGroupWhenFixedOnGridIsTrue()
    {
        var block = new SimulatedBlock();
        block.LocationInTheGroup = new Coord(0, 2);
        block.FixedOnGrid = true;
        block.SetLocation(new Coord(1, 2));
        Assert.AreEqual(new Coord(1, 2), block.Location);
    }

    [Test]
    public void ShouldSimulateExistedBlock()
    {
        IBlock block = Substitute.For<IBlock>();
        block.Number.Returns(7);
        block.BlockType.Returns(BlockType.Seven);
        block.Location.Returns(new Coord(2, 5));
        block.LocationInTheGroup.Returns(new Coord(0, 1));

        simulatedBlock.SimulateFromBlock(block);

        Assert.AreEqual(simulatedBlock.Number, block.Number);
        Assert.AreEqual(simulatedBlock.BlockType, block.BlockType);
        Assert.AreEqual(simulatedBlock.Location, block.Location);
        Assert.AreEqual(simulatedBlock.LocationInTheGroup, block.LocationInTheGroup);
    }
    
}
