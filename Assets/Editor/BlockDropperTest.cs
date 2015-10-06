using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class BlockDropperTest : GridTestFixture
{

    [Test]
    public void ShouldReturnDroppedGrid()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.FixGroup();

        IBlock[,] blocks = new IBlock[grid.Width, grid.Height];

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                blocks[x, y] = grid[x, y];
            }
        }

        Assert.AreEqual(blocks[3, 13], group.Children[0]);
        Assert.AreEqual(blocks[3, 12], group.Children[1]);
        Assert.AreEqual(blocks[4, 13], group.Children[2]);
        Assert.AreEqual(blocks[2, 13], group.Children[3]);

        bool dropped;
        IBlock[,] blocksAfterDropped = BlockDropper.GetGridAfterDrop(blocks, out dropped);

        Assert.IsTrue(dropped);
        Assert.AreEqual(blocksAfterDropped[3, 1], group.Children[0]);
        Assert.AreEqual(blocksAfterDropped[3, 0], group.Children[1]);
        Assert.AreEqual(blocksAfterDropped[4, 0], group.Children[2]);
        Assert.AreEqual(blocksAfterDropped[2, 0], group.Children[3]);
    }
	
    [Test]
    public void ReturnFalseWhenDroppedNothing()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.FixGroup();

        IBlock[,] blocks = new IBlock[grid.Width, grid.Height];

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                blocks[x, y] = grid[x, y];
            }
        }

        Assert.AreEqual(blocks[3, 13], group.Children[0]);
        Assert.AreEqual(blocks[3, 12], group.Children[1]);
        Assert.AreEqual(blocks[4, 13], group.Children[2]);
        Assert.AreEqual(blocks[2, 13], group.Children[3]);

        bool dropped;
        IBlock[,] blocksAfterDropped = BlockDropper.GetGridAfterDrop(blocks, out dropped);

        Assert.IsTrue(dropped);
        Assert.AreEqual(blocksAfterDropped[3, 1], group.Children[0]);
        Assert.AreEqual(blocksAfterDropped[3, 0], group.Children[1]);
        Assert.AreEqual(blocksAfterDropped[4, 0], group.Children[2]);
        Assert.AreEqual(blocksAfterDropped[2, 0], group.Children[3]);

        BlockDropper.GetGridAfterDrop(blocksAfterDropped, out dropped);
        Assert.IsFalse(dropped);
    }
}
