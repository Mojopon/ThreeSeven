using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GridEventManagerTest : GridTestFixture 
{
    IGridEventManager gridEventManager;
    IGrid gridMock;

    [SetUp]
    public void Init()
    {
        gridMock = Substitute.For<IGrid>();
        gridEventManager = new GridEventManager(setting, gridMock, groupFactory);
    }

    [Test]
    public void ShouldAddGroupToTheGrid()
    {
        gridEventManager.AddGroupToTheGrid(blockPattern, groupPattern);

        gridMock.Received().AddGroup(Arg.Any<IGroup>());
    }

    [Test]
    public void ShouldAddRandomGroupToTheGrid()
    {
        gridEventManager.AddGroupToTheGrid();

        gridMock.Received().AddGroup(Arg.Any<IGroup>());
    }

    [Test]
    public void ShouldAddGroupWhenReady()
    {
        gridMock.CurrenteStateName.Returns(GridStates.ReadyForNextGroup);

        gridEventManager.OnUpdate();

        gridMock.Received().AddGroup(Arg.Any<IGroup>());
    }

    [Test]
    public void ShouldStartDeletingWhenGridStateIsDropped()
    {
        gridMock.CurrenteStateName.Returns(GridStates.Dropped);

        gridEventManager.OnUpdate();

        gridMock.Received().StartDeleting();
    }

    [Test]
    public void ShouldStartDroppingWhenGridStateIsDeleted()
    {
        gridMock.CurrenteStateName.Returns(GridStates.Deleted);

        gridEventManager.OnUpdate();

        gridMock.Received().DropBlocks();
    }

    [Test]
    public void ShouldStartDroppingAfterFix()
    {
        gridMock.CurrenteStateName.Returns(GridStates.OnFix);

        gridEventManager.OnUpdate();

        gridMock.Received().DropBlocks();
    }

    [Test]
    public void ShouldMoveBlocksWhenDropping()
    {
        gridMock.CurrenteStateName.Returns(GridStates.Dropping);

        gridEventManager.OnUpdate();

        gridMock.Received().MoveBlocks();
    }

    [Test]
    public void ShouldDoDeleteEffectWhenDeleting()
    {
        gridMock.CurrenteStateName.Returns(GridStates.Deleting);

        gridEventManager.OnUpdate();

        gridMock.Received().ProcessDeleting();
    }

    [Test]
    public void ShouldBeGameOverWhenCannotAddGroup()
    {
        gridMock.AddGroup(group).Returns(false);
        gridMock.CurrenteStateName.Returns(GridStates.ReadyForNextGroup);

        gridEventManager.OnUpdate();

        gridMock.Received().GameOver();
        gridMock.CurrenteStateName.Returns(GridStates.GameOver);

        gridEventManager.OnUpdate();
        Assert.IsTrue(gridEventManager.GameIsOver);
    }
}
