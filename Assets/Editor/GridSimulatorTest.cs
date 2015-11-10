using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GridSimulatorTest : GridTestFixture
{
    IGridSimulator gridSimulator;

    [SetUp]
    public void InitializeGridSimulator()
    {
        blockPattern = Substitute.For<IBlockPattern>();
        blockTypeMock = new BlockType[]
        {
            BlockType.One,
            BlockType.Six,
            BlockType.Three,
            BlockType.Five,
        };
        blockPattern.Types.Returns(blockTypeMock);

        group = groupFactory.Create(setting, blockPattern, groupPattern);
        
        gridSimulator = new GridSimulator(grid, setting);
    }

    [Test]
    public void ShouldCreateSameGridPatternWhenSimulate()
    {
        AddBlockMock(0, 0, 5);
        AddBlockMock(1, 0, 3);

        Assert.AreEqual(5, grid[0, 0].Number);
        Assert.AreEqual(3, grid[1, 0].Number);

        gridSimulator.CreateSimulatedGridOriginal();

        for(int y = 0; y < grid.Height; y++)
        {
            for(int x = 0; x < grid.Width; x++)
            {
                if (grid[x, y] != null)
                {
                    Assert.AreEqual(gridSimulator.SimulatedGrid[x, y].Number, grid[x, y].Number);
                }
            }
        }
    }

    [Test]
    public void ShouldDropSimulatedBlocksToBottom()
    {
        Assert.AreEqual(7, setting.GridWidth);
        Assert.AreEqual(14, setting.GridHeight);

        var blockMockOne = AddBlockMock(0, 12, 5);
        var blockMockTwo = AddBlockMock(1, 12, 3);

        gridSimulator.CreateSimulatedGridOriginal();
        Assert.IsTrue(gridSimulator.DropBlocks());

        Assert.AreEqual(blockMockOne.Number, gridSimulator.SimulatedGrid[0, 0].Number);
        Assert.AreEqual(blockMockTwo.Number, gridSimulator.SimulatedGrid[1, 0].Number);
        Assert.AreEqual(new Coord(0, 0), gridSimulator.SimulatedGrid[0, 0].Location);
        Assert.AreEqual(new Coord(1, 0), gridSimulator.SimulatedGrid[1, 0].Location);
    }

    [Test]
    public void ShouldResetToOriginal()
    {
        Assert.AreEqual(7, setting.GridWidth);
        Assert.AreEqual(14, setting.GridHeight);

        var blockMockOne = AddBlockMock(0, 12, 5);
        var blockMockTwo = AddBlockMock(1, 12, 3);

        gridSimulator.CreateSimulatedGridOriginal();

        Assert.AreEqual(blockMockOne.Number, gridSimulator.SimulatedGrid[0, 12].Number);
        Assert.AreEqual(blockMockTwo.Number, gridSimulator.SimulatedGrid[1, 12].Number);

        Assert.IsTrue(gridSimulator.DropBlocks());

        Assert.AreEqual(blockMockOne.Number, gridSimulator.SimulatedGrid[0, 0].Number);
        Assert.AreEqual(blockMockTwo.Number, gridSimulator.SimulatedGrid[1, 0].Number);
        Assert.AreEqual(new Coord(0, 0), gridSimulator.SimulatedGrid[0, 0].Location);
        Assert.AreEqual(new Coord(1, 0), gridSimulator.SimulatedGrid[1, 0].Location);

        gridSimulator.CopyOriginalToSimulatedGrid();

        Assert.AreEqual(blockMockOne.Number, gridSimulator.SimulatedGrid[0, 12].Number);
        Assert.AreEqual(blockMockTwo.Number, gridSimulator.SimulatedGrid[1, 12].Number);
        Assert.AreEqual(new Coord(0, 12), gridSimulator.SimulatedGrid[0, 12].Location);
        Assert.AreEqual(new Coord(1, 12), gridSimulator.SimulatedGrid[1, 12].Location);

    }

    [Test]
    public void ShouldDeleteSimulatedBlocks()
    {
        Assert.AreEqual(7, setting.GridWidth);
        Assert.AreEqual(14, setting.GridHeight);

        var blockMockOne = AddBlockMock(0, 0, 1);
        var blockMockTwo = AddBlockMock(1, 0, 2);
        var blockMockThree = AddBlockMock(2, 0, 4);

        gridSimulator.CreateSimulatedGridOriginal();

        var simulatedBlockOne = gridSimulator.SimulatedGrid[0, 0];
        var simulatedBlockTwo= gridSimulator.SimulatedGrid[1, 0];
        var simulatedBlockThree = gridSimulator.SimulatedGrid[2, 0];

        Assert.AreEqual(blockMockOne.Number, simulatedBlockOne.Number);
        Assert.AreEqual(blockMockTwo.Number, simulatedBlockTwo.Number);
        Assert.AreEqual(blockMockThree.Number, simulatedBlockThree.Number);

        var blocksToDelete = gridSimulator.DeleteBlocks();

        Assert.IsTrue(blocksToDelete.Contains(simulatedBlockOne));
        Assert.IsTrue(blocksToDelete.Contains(simulatedBlockTwo));
        Assert.IsTrue(blocksToDelete.Contains(simulatedBlockThree));

        Assert.IsNull(gridSimulator.SimulatedGrid[0, 0]);
        Assert.IsNull(gridSimulator.SimulatedGrid[1, 0]);
        Assert.IsNull(gridSimulator.SimulatedGrid[2, 0]);
    }

    [Test]
    public void ShouldDeleteSimulatedBlocksThreeSeven()
    {
        var blockMockOne = AddBlockMock(0, 0, 7);
        var blockMockTwo = AddBlockMock(1, 0, 7);
        var blockMockThree = AddBlockMock(2, 0, 7);

        gridSimulator.CreateSimulatedGridOriginal();

        var simulatedBlockOne = gridSimulator.SimulatedGrid[0, 0];
        var simulatedBlockTwo = gridSimulator.SimulatedGrid[1, 0];
        var simulatedBlockThree = gridSimulator.SimulatedGrid[2, 0];

        Assert.AreEqual(blockMockOne.Number, simulatedBlockOne.Number);
        Assert.AreEqual(blockMockTwo.Number, simulatedBlockTwo.Number);
        Assert.AreEqual(blockMockThree.Number, simulatedBlockThree.Number);

        var blocksToDelete = gridSimulator.DeleteBlocks();

        Assert.IsTrue(blocksToDelete.Contains(simulatedBlockOne));
        Assert.IsTrue(blocksToDelete.Contains(simulatedBlockTwo));
        Assert.IsTrue(blocksToDelete.Contains(simulatedBlockThree));

        Assert.IsNull(gridSimulator.SimulatedGrid[0, 0]);
        Assert.IsNull(gridSimulator.SimulatedGrid[1, 0]);
        Assert.IsNull(gridSimulator.SimulatedGrid[2, 0]);
    }

    [Test]
    public void ShouldSimulateGroupInTheGrid()
    {
        Assert.IsTrue(grid.AddGroup(group));

        gridSimulator.CreateSimulatedGridOriginal();

        Assert.IsNotNull(gridSimulator.SimulatedGroup);

        for(int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(group.Children[i].BlockType, gridSimulator.SimulatedGroup.Children[i].BlockType);
            Assert.AreEqual(group.Children[i].Number, gridSimulator.SimulatedGroup.Children[i].Number);
            Assert.AreEqual(group.Children[i].Location, gridSimulator.SimulatedGroup.Children[i].Location);
            Assert.AreEqual(group.Children[i].LocationInTheGroup, gridSimulator.SimulatedGroup.Children[i].LocationInTheGroup);
        }
    }

    [Test]
    public void ShouldReturnScoreResultedFromTheSimulation()
    {
        Assert.IsTrue(grid.AddGroup(group));
        int expectedScore = 70;

        gridSimulator.CreateSimulatedGridOriginal();

        int result = gridSimulator.GetScoreFromSimulation();
        Assert.AreEqual(expectedScore, result);
    }

    [Test]
    public void SimulationShouldntChangeGridState()
    {
        Assert.IsTrue(grid.AddGroup(group));
        
        gridSimulator.CreateSimulatedGridOriginal();

        Assert.AreEqual(grid.CurrentGroup.Children.Length, gridSimulator.SimulatedGroup.Children.Length);
        for(int i = 0; i < grid.CurrentGroup.Children.Length; i++)
        {
            Assert.AreEqual(grid.CurrentGroup.Children[i].Number, gridSimulator.SimulatedGroup.Children[i].Number);
            Assert.AreEqual(grid.CurrentGroup.Children[i].BlockType, gridSimulator.SimulatedGroup.Children[i].BlockType);
            Assert.AreEqual(grid.CurrentGroup.Children[i].Location, gridSimulator.SimulatedGroup.Children[i].Location);
            Assert.AreEqual(grid.CurrentGroup.Children[i].LocationInTheGroup, gridSimulator.SimulatedGroup.Children[i].LocationInTheGroup);
        }

        for(int y = 0; y < setting.GridHeight; y++)
        {
            for(int x = 0; x < setting.GridWidth; x++)
            {
                if (grid[x, y] == null) continue;
                Assert.AreEqual(grid[x, y].Number, gridSimulator.SimulatedGrid[x, y].Number);
                Assert.AreEqual(grid[x, y].BlockType, gridSimulator.SimulatedGrid[x, y].BlockType);
                Assert.AreEqual(grid[x, y].Location, gridSimulator.SimulatedGrid[x, y].Location);
            }
        }

        gridSimulator.GetScoreFromSimulation();

        Assert.AreEqual(grid.CurrentGroup.Children.Length, gridSimulator.SimulatedGroup.Children.Length);
        for (int i = 0; i < grid.CurrentGroup.Children.Length; i++)
        {
            Assert.AreEqual(grid.CurrentGroup.Children[i].Number, gridSimulator.SimulatedGroup.Children[i].Number);
            Assert.AreEqual(grid.CurrentGroup.Children[i].BlockType, gridSimulator.SimulatedGroup.Children[i].BlockType);
            Assert.AreEqual(grid.CurrentGroup.Children[i].Location, gridSimulator.SimulatedGroup.Children[i].Location);
            Assert.AreEqual(grid.CurrentGroup.Children[i].LocationInTheGroup, gridSimulator.SimulatedGroup.Children[i].LocationInTheGroup);
        }

        for (int y = 0; y < setting.GridHeight; y++)
        {
            for (int x = 0; x < setting.GridWidth; x++)
            {
                if (grid[x, y] == null) continue;
                Assert.AreEqual(grid[x, y].Number, gridSimulator.SimulatedGrid[x, y].Number);
                Assert.AreEqual(grid[x, y].BlockType, gridSimulator.SimulatedGrid[x, y].BlockType);
                Assert.AreEqual(grid[x, y].Location, gridSimulator.SimulatedGrid[x, y].Location);
            }
        }
    }

    [Test]
    public void ShouldCheckIfCanSetGroupToTheLocation()
    {
        Assert.IsTrue(grid.AddGroup(group));

        gridSimulator.CreateSimulatedGridOriginal();

        Assert.IsTrue(gridSimulator.SetGroupLocation(grid.CurrentGroup.Location + Direction.Right.ToCoord()));
        for (int i = 0; i < grid.CurrentGroup.Children.Length; i++)
        {
            Assert.AreEqual(grid.CurrentGroup.Children[i].Location + Direction.Right.ToCoord(), gridSimulator.SimulatedGroup.Children[i].Location);
        }

        Assert.IsFalse(gridSimulator.SetGroupLocation(grid.CurrentGroup.Location + new Coord(3, 0)));

        for (int i = 0; i < grid.CurrentGroup.Children.Length; i++)
        {
            Assert.AreEqual(grid.CurrentGroup.Children[i].Location + Direction.Right.ToCoord(), gridSimulator.SimulatedGroup.Children[i].Location);
        }
    }

    [Test]
    public void ShouldCheckIfCanRotateTheGroup()
    {
        Assert.IsTrue(grid.AddGroup(group));
        gridSimulator.CreateSimulatedGridOriginal();

        Assert.IsFalse(gridSimulator.RotateGroup());

        for (int i = 0; i < grid.CurrentGroup.Children.Length; i++)
        {
            Assert.AreEqual(grid.CurrentGroup.Children[i].Number, gridSimulator.SimulatedGroup.Children[i].Number);
            Assert.AreEqual(grid.CurrentGroup.Children[i].BlockType, gridSimulator.SimulatedGroup.Children[i].BlockType);
            Assert.AreEqual(grid.CurrentGroup.Children[i].Location, gridSimulator.SimulatedGroup.Children[i].Location);
            Assert.AreEqual(grid.CurrentGroup.Children[i].LocationInTheGroup, gridSimulator.SimulatedGroup.Children[i].LocationInTheGroup);
        }

        Assert.Fail();
    }

    [Test]
    public void SimulationTestOne()
    {
        AddBlockMock(3, 0, 6);
        AddBlockMock(3, 1, 6);

        blockPattern = Substitute.For<IBlockPattern>();
        blockTypeMock = new BlockType[]
        {
            BlockType.One,
            BlockType.One,
            BlockType.Seven,
            BlockType.Seven,
        };
        blockPattern.Types.Returns(blockTypeMock);

        group = groupFactory.Create(setting, blockPattern, groupPattern);

        grid.AddGroup(group);
        gridSimulator.CreateSimulatedGridOriginal();

        Assert.AreEqual(1, gridSimulator.SimulatedGroup.Children[0].Number);
        Assert.AreEqual(1, gridSimulator.SimulatedGroup.Children[1].Number);
        Assert.AreEqual(7, gridSimulator.SimulatedGroup.Children[2].Number);
        Assert.AreEqual(7, gridSimulator.SimulatedGroup.Children[3].Number);

        Assert.AreEqual(6, gridSimulator.SimulatedGrid[3, 0].Number);
        Assert.AreEqual(6, gridSimulator.SimulatedGrid[3, 1].Number);

        int expectedScore = ((1 + 6) * 10) + (((1 + 6) * 10) * 2);

        var result = gridSimulator.GetScoreFromSimulation();
        Assert.AreEqual(expectedScore, result);
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
