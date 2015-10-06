using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class IBlockTest : ThreeSevenTestFixsture
{
    IBlock block;

    [SetUp]
    public void Init()
    {
        block = blockFactory.Create(setting, BlockType.Three, new Coord(1, 0));
    }

    [Test]
    public void ShouldReturnBlockType()
    {
        Assert.AreEqual(BlockType.Three, block.BlockType);
        Assert.AreEqual(new Coord(1, 0), block.OriginalLocation);
    }

    [Test]
    public void ShouldReturnBlockNumber()
    {
        Assert.AreEqual(3, block.Number);
    }
	
    [Test]
    public void ShouldSetOriginalLocationToBeDefault()
    {
        block.OnFix();
        Assert.AreEqual(new Coord(0, 0), block.OriginalLocation);
    }

    [Test]
    public void ViewReceivesDeleteWhenDeleteBlock()
    {
        IBlockView blockView = Substitute.For<IBlockView>();
        block.AttachView(blockView);
        block.StartDeleting();
        blockView.Received().Delete();
    }

    [Test]
    public void ShouldReturnTrueWhenBlockViewIsOnMove()
    {
        Assert.IsFalse(block.IsOnMove);

        IBlockView blockView = Substitute.For<IBlockView>();
        blockView.IsOnMove.Returns(true);
        block.AttachView(blockView);

        Assert.IsTrue(block.IsOnMove);
    }

    [Test]
    public void ViewShouldReceiveMoveTo()
    {
        IBlockView blockView = Substitute.For<IBlockView>();
        block.AttachView(blockView);
        Coord coord = new Coord(2, 2);
        block.MoveToLocation(coord);

        blockView.Received().MoveTo(Arg.Any<Vector2>());
    }

    [Test]
    public void NullBlockViewShouldReturnIsToDeleteToBeTrueWhenDelete()
    {
        NullBlockView nullBlockView = new NullBlockView();
        Assert.IsFalse(nullBlockView.IsToDelete);
        nullBlockView.Delete();
        Assert.IsTrue(nullBlockView.IsToDelete);
    }

    [Test]
    public void ShouldReturnIsDeletingTrueWhenViewReturnsTrue()
    {
        IBlockView blockView = Substitute.For<IBlockView>();
        blockView.IsDeleting.Returns(true);
        block.AttachView(blockView);

        block.IsDeleting.Returns(true);

        blockView.IsDeleting.Returns(false);
        block.IsDeleting.Returns(false);
    }
    
    [Test]
    public void ShouldReturnIsToDeleteTrueWhenViewReturnsTrue()
    {
        IBlockView blockView = Substitute.For<IBlockView>();
        blockView.IsDeleting.Returns(true);
        block.AttachView(blockView);

        block.IsToDelete.Returns(true);

        blockView.IsToDelete.Returns(false);
        block.IsToDelete.Returns(false);
    }
}
