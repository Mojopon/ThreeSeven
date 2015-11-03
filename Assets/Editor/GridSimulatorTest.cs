﻿using UnityEngine;
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
        gridSimulator = new GridSimulator(grid, setting);
    }

    [Test]
    public void ShouldCreateSameGridPatternWhenSimulate()
    {
        AddBlockMock(0, 0, 5);
        AddBlockMock(1, 0, 3);

        Assert.AreEqual(5, grid[0, 0].Number);
        Assert.AreEqual(3, grid[1, 0].Number);

        gridSimulator.Simulate();

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

    IBlock AddBlockMock(int x, int y, int number)
    {
        IBlock blockMock = Substitute.For<IBlock>();
        blockMock.Number.Returns(number);
        grid[x, y] = blockMock;

        return blockMock;
    }
}