using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using System.Threading;

[TestFixture]
public class CPUBehaviourTest : GridTestFixture
{

    [Test]
    public void ShouldOutputBestLocationToGetScore()
    {
        IGridSimulator gridSimulator;

        blockPattern = Substitute.For<IBlockPattern>();
        blockTypeMock = new BlockType[]
        {
            BlockType.Seven,
            BlockType.Seven,
            BlockType.Six,
            BlockType.Five,
        };
        blockPattern.Types.Returns(blockTypeMock);
        group = groupFactory.Create(setting, blockPattern, groupPattern);

        AddBlockMock(5, 0, 7);

        var smartCPU = new SmartCPUBehaviour(grid, setting, CPUMode.Kusotuyo); 

        grid.AddGroup(group);
        var groupLocation = group.Location;

        smartCPU.ProcessMovement();
        groupLocation += Direction.Right.ToCoord();
        Assert.AreEqual(groupLocation, group.Location);

        smartCPU.ProcessMovement();
        groupLocation += Direction.Right.ToCoord();
        Assert.AreEqual(groupLocation, group.Location);

        smartCPU.ProcessMovement();
        groupLocation += Direction.Down.ToCoord();
        Assert.AreEqual(groupLocation, group.Location);
    }

    IBlock AddBlockMock(int x, int y, int number)
    {
        IBlock blockMock = Substitute.For<IBlock>();
        blockMock.Number.Returns(number);
        blockMock.Location.Returns(new Coord(x, y));
        grid[x, y] = blockMock;

        return blockMock;
    }
}
