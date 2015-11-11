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

        gridSimulator = new GridSimulator(grid, setting);
        cpuManager = new CPUManager(grid, setting);

        grid.AddGroup(group);
        AddBlockMock(5, 0, 7);

        var cpu = new OutputBestMovement(gridSimulator);
        var movements = cpu.Output();
        Assert.AreEqual(2, movements.Count);
        Assert.AreEqual(Direction.Right, movements[0]);
        Assert.AreEqual(Direction.Right, movements[1]);

        Assert.Fail();
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
