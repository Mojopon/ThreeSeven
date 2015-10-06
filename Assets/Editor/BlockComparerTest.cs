using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class BlockComparerTest : GridTestFixture
{
    [Test]
    public void ShouldReturnBlocksToDelete()
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

        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));

        Assert.IsTrue(group.Children[0].Number == 1);
        Assert.IsTrue(group.Children[1].Number == 6);

        grid.FixGroup();
        grid.DropBlocks();

        Assert.AreEqual(new Coord(3, 1), group.Children[0].Location);
        Assert.AreEqual(new Coord(3, 0), group.Children[1].Location);
        Assert.AreEqual(new Coord(4, 0), group.Children[2].Location);
        Assert.AreEqual(new Coord(2, 0), group.Children[3].Location);

        IBlock[,] blocks = new IBlock[grid.Width, grid.Height];

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                blocks[x, y] = grid[x, y];
            }
        }

        Assert.IsTrue(blocks[3, 1].Number == 1);
        Assert.IsTrue(blocks[3, 0].Number == 6);

        var toDelete = BlockComparer.Compare(blocks);
        Assert.IsTrue(toDelete.Contains(group.Children[0]));
        Assert.IsTrue(toDelete.Contains(group.Children[1]));
        Assert.IsFalse(toDelete.Contains(group.Children[2]));
        Assert.IsFalse(toDelete.Contains(group.Children[3]));
    }

    [Test]
    public void ShouldAddThreeSevenToDelete()
    {
        blockPattern = Substitute.For<IBlockPattern>();
        blockTypeMock = new BlockType[] 
        {
            BlockType.One,
            BlockType.Seven,
            BlockType.Seven,
            BlockType.Seven,
        };
        blockPattern.Types.Returns(blockTypeMock);

        group = groupFactory.Create(setting, blockPattern, groupPattern);

        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));

        Assert.IsTrue(group.Children[1].Number == 7);
        Assert.IsTrue(group.Children[2].Number == 7);
        Assert.IsTrue(group.Children[3].Number == 7);

        grid.FixGroup();
        grid.DropBlocks();

        Assert.AreEqual(new Coord(3, 1), group.Children[0].Location);
        Assert.AreEqual(new Coord(3, 0), group.Children[1].Location);
        Assert.AreEqual(new Coord(4, 0), group.Children[2].Location);
        Assert.AreEqual(new Coord(2, 0), group.Children[3].Location);

        IBlock[,] blocks = new IBlock[grid.Width, grid.Height];

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                blocks[x, y] = grid[x, y];
            }
        }

        Assert.IsTrue(blocks[3, 1].Number == 1);
        Assert.IsTrue(blocks[3, 0].Number == 7);
        Assert.IsTrue(blocks[4, 0].Number == 7);
        Assert.IsTrue(blocks[2, 0].Number == 7);

        var toDelete = BlockComparer.Compare(blocks);
        Assert.IsFalse(toDelete.Contains(group.Children[0]));
        Assert.IsTrue(toDelete.Contains(group.Children[1]));
        Assert.IsTrue(toDelete.Contains(group.Children[2]));
        Assert.IsTrue(toDelete.Contains(group.Children[3]));
    }
	
}
