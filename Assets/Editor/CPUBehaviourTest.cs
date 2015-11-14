using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class CPUBehaviourTest : GridTestFixture
{

    [Test]
    public void ShouldOutputBestLocationToGetScore()
    {
        IGridSimulator gridSimulator;
        ICPUManager cpuManager;

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

        var smartCPU = new SmartCPUBehaviour(grid, setting, CPUMode.Easy); 

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

    [Test]
    public void ShouldInputMoveAction()
    {
        IGrid gridMock = Substitute.For<IGrid>();
        ICPUManager cpuManager = new CPUManager(gridMock, setting);

        cpuManager.ChangeCPUMode(CPUMode.Easy);
        cpuManager.OnUpdate();

        gridMock.Received().OnArrowKeyInput(Arg.Any<Direction>());
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
