using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GridTest : GridTestFixture
{

    [Test]
    public void ShouldReturnLengthOfTheGrid()
    {
        Assert.AreEqual(setting.GridWidth, grid.Width);
        Assert.AreEqual(setting.GridHeight, grid.Height);
    }

    [Test]
    public void ShouldChangeGridStateWhenGroupIsAdded()
    {
        grid.SetState(GridStates.ReadyForNextGroup);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.OnControlGroup);
    }

    [Test]
    public void CanMoveAddedGroup()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));

        for (int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Assert.AreEqual(group.Location + locationMock[0][i], group.ChildrenLocation[i]);
        }

        grid.OnArrowKeyInput(Direction.Right);
        for (int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Assert.AreEqual(group.Location + locationMock[0][i], group.ChildrenLocation[i]);
        }
    }

    [Test]
    public void GroupShouldReceiveFixWhenFixGroup()
    {
        IGroup groupMock = Substitute.For<IGroup>();
        Assert.IsTrue(grid.AddGroup(groupMock));
        grid.FixGroup();
        groupMock.Received().Fix();
    }

    [Test]
    public void CantMoveOutofTheGrid()
    {
        Assert.IsTrue(setting.GridWidth == 7);

        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));

        grid.OnArrowKeyInput(Direction.Right);
        grid.OnArrowKeyInput(Direction.Right);

        Coord[] locations = new Coord[group.ChildrenLocation.Length];
        for (int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Coord vector = Direction.Right.ToCoord() + Direction.Right.ToCoord();
            Assert.AreEqual(locationMock[0][i] + setting.BlockSpawnPoint + vector, group.ChildrenLocation[i]);
            locations[i] = group.ChildrenLocation[i];
        }

        grid.OnArrowKeyInput(Direction.Right);

        for (int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Assert.AreEqual(locations[i], group.ChildrenLocation[i]);
        }
    }

    [Test]
    public void ShouldFixCurrentGroup()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));

        for (int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Assert.AreEqual(group.Location + locationMock[0][i], group.ChildrenLocation[i]);
        }

        grid.SetState(GridStates.OnFix);
        grid.OnUpdate();
        for (int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Coord location = group.ChildrenLocation[i];
            Assert.IsFalse(grid.IsAvailable(location.X, location.Y));
        }

        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropping);
    }

    [Test]
    public void CantAddGroupWhenThePlaceIsUsed()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.FixGroup();
        IGroup groupTwo = groupFactory.Create(setting, blockPattern, groupPattern);
        Assert.IsFalse(grid.AddGroup(groupTwo));
    }

    [Test]
    public void ShouldDropAllBlocks()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));


        Assert.AreEqual(new Coord(3, 13), group.Children[0].Location);
        Assert.AreEqual(new Coord(3, 12), group.Children[1].Location);
        Assert.AreEqual(new Coord(4, 13), group.Children[2].Location);
        Assert.AreEqual(new Coord(2, 13), group.Children[3].Location);

        grid.FixGroup();
        grid.DropBlocks();

        Assert.AreEqual(new Coord(3, 1), group.Children[0].Location);
        Assert.AreEqual(new Coord(3, 0), group.Children[1].Location);
        Assert.AreEqual(new Coord(4, 0), group.Children[2].Location);
        Assert.AreEqual(new Coord(2, 0), group.Children[3].Location);
    }

    [Test]
    public void ShouldChangeStateWhenBlockIsDropped()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.SetState(GridStates.OnControlGroup);
        Assert.IsTrue(grid.CurrenteStateName == GridStates.OnControlGroup);
        grid.SetState(GridStates.OnFix);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropping);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropped);
    }

    [Test]
    public void ShouldDeleteAllBlocks()
    {
        Assert.IsFalse(group.Children[0].IsToDelete);
        group.Children[0].StartDeleting();
        Assert.IsTrue(group.Children[0].IsToDelete);

        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));

        Assert.AreEqual(new Coord(3, 13), group.Children[0].Location);
        Assert.AreEqual(new Coord(3, 12), group.Children[1].Location);
        Assert.AreEqual(new Coord(4, 13), group.Children[2].Location);
        Assert.AreEqual(new Coord(2, 13), group.Children[3].Location);

        grid.FixGroup();
        Assert.IsTrue(grid.DropBlocks());

        Assert.AreEqual(new Coord(3, 1), group.Children[0].Location);
        Assert.AreEqual(new Coord(3, 0), group.Children[1].Location);
        Assert.AreEqual(new Coord(4, 0), group.Children[2].Location);
        Assert.AreEqual(new Coord(2, 0), group.Children[3].Location);

        Assert.AreEqual(1, grid[3, 1].Number);
        Assert.AreEqual(6, grid[3, 0].Number);

        grid.SetState(GridStates.Dropped);
        Assert.IsNotNull(grid[3, 1]);
        Assert.IsNotNull(grid[3, 0]);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleting);
        grid.OnUpdate();
        Assert.IsNull(grid[3, 1]);
        Assert.IsNull(grid[3, 0]);
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleted);
    }

    [Test]
    public void ShouldChangeStateWhenBlockIsDeleted()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.SetState(GridStates.OnControlGroup);
        Assert.IsTrue(grid.CurrenteStateName == GridStates.OnControlGroup);
        grid.FixGroup();
        grid.SetState(GridStates.Deleting);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleted);
    }

    [Test]
    public void ShouldChangeStateToBeReadyWhenNothingIsDeleted()
    {
        grid.SetState(GridStates.Dropped);
        Assert.IsFalse(grid.StartDeleting());
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.ReadyForNextGroup);
    }

    [Test]
    public void ShouldMoveBlockViewsWhenDropBlocks()
    {
        IBlockView blockViewOne = Substitute.For<IBlockView>();
        IBlockView blockViewTwo = Substitute.For<IBlockView>();
        IBlockView blockViewThree = Substitute.For<IBlockView>();
        IBlockView blockViewFour = Substitute.For<IBlockView>();
        group.Children[0].AttachView(blockViewOne);
        group.Children[1].AttachView(blockViewTwo);
        group.Children[2].AttachView(blockViewThree);
        group.Children[3].AttachView(blockViewFour);
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.SetState(GridStates.OnFix);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropping);
        blockViewOne.Received().MoveTo(group.Children[0].Location.ToVector2());
        blockViewTwo.Received().MoveTo(group.Children[1].Location.ToVector2());
        blockViewThree.Received().MoveTo(group.Children[2].Location.ToVector2());
        blockViewFour.Received().MoveTo(group.Children[3].Location.ToVector2());
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropped);
        blockViewOne.Received().OnUpdate();
        blockViewTwo.Received().OnUpdate();
        blockViewThree.Received().OnUpdate();
        blockViewFour.Received().OnUpdate();
    }

    [Test]
    public void StayDroppingStateWhenBlockIsStillMoving()
    {
        IBlockView blockViewOne = Substitute.For<IBlockView>();
        IBlockView blockViewTwo = Substitute.For<IBlockView>();
        IBlockView blockViewThree = Substitute.For<IBlockView>();
        IBlockView blockViewFour = Substitute.For<IBlockView>();
        blockViewOne.IsOnMove.Returns(true);
        group.Children[0].AttachView(blockViewOne);
        group.Children[1].AttachView(blockViewTwo);
        group.Children[2].AttachView(blockViewThree);
        group.Children[3].AttachView(blockViewFour);
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.SetState(GridStates.OnFix);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropping);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropping);
        blockViewOne.IsOnMove.Returns(false);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropped);
    }

    [Test]
    public void StayDeletingStateWhenBlockIsStillOnDeleteEffect()
    {
        IBlockView blockViewOne = Substitute.For<IBlockView>();
        IBlockView blockViewTwo = Substitute.For<IBlockView>();
        blockViewOne.IsToDelete.Returns(true);
        blockViewTwo.IsToDelete.Returns(true);
        group.Children[0].AttachView(blockViewOne);
        group.Children[1].AttachView(blockViewTwo);
        blockViewOne.IsDeleting.Returns(true);
        blockViewTwo.IsDeleting.Returns(true);

        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.SetState(GridStates.OnFix);
        Assert.IsTrue(grid.CurrenteStateName == GridStates.OnFix);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropping);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Dropped);

        Assert.AreEqual(1, grid[3, 1].Number);
        Assert.AreEqual(6, grid[3, 0].Number);

        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleting);

        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleting);

        blockViewOne.IsDeleting.Returns(false);
        blockViewTwo.IsDeleting.Returns(false);

        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleted);
    }

    [Test]
    public void ReturnFalseWhenNothingToDelete()
    {
        Assert.IsFalse(grid.StartDeleting());
    }

    [Test]
    public void CanDropMultipleTimes()
    {
        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.SetState(GridStates.OnControlGroup);

        for (int i = 0; i < 20; i++)
        {
            grid.OnArrowKeyInput(Direction.Down);
        }

        Assert.AreEqual(new Coord(3, 1), group.Children[0].Location);
        Assert.AreEqual(new Coord(3, 0), group.Children[1].Location);
        Assert.AreEqual(new Coord(4, 1), group.Children[2].Location);
        Assert.AreEqual(new Coord(2, 1), group.Children[3].Location);


        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.OnFix);
    }

    [Test]
    public void ShouldIncrementAndResetChains()
    {
        Assert.AreEqual(0, grid.Chains);
        grid.IncrementChains();
        Assert.AreEqual(1, grid.Chains);

        grid.SetState(GridStates.ReadyForNextGroup);
        grid.OnUpdate();
        Assert.AreEqual(0, grid.Chains);

    }

    [Test]
    public void CanSetBlock()
    {
        Assert.IsNull(grid[3, 3]);
        grid[3, 3] = Substitute.For<IBlock>();
        Assert.IsNotNull(grid[3, 3]);
    }

    [Test]
    public void ShouldRenderFloatingTextWhenDelete()
    {
        IFloatingTextRenderer floatingTextRenderer = Substitute.For<IFloatingTextRenderer>();
        setting.FloatingTextRenderer = floatingTextRenderer;
        var gridFactory = new GridFactory(setting, groupFactory);
        grid = gridFactory.Create();
        grid.NewGame();

        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.FixGroup();
        Assert.IsTrue(grid.DropBlocks());

        grid.SetState(GridStates.Dropped);
        Assert.IsNotNull(grid[3, 1]);
        Assert.IsNotNull(grid[3, 0]);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleting);
        grid.OnUpdate();
        Assert.IsNull(grid[3, 1]);
        Assert.IsNull(grid[3, 0]);
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleted);

        floatingTextRenderer.Received().RenderText(Arg.Any<Vector2>(), Arg.Any<string>());
    }

    [Test]
    public void ShouldBePaused()
    {
        GridStates stateBeforePause = grid.CurrenteStateName;

        grid.Pause();
        Assert.AreEqual(GridStates.Paused, grid.CurrenteStateName);

        grid.Pause();
        Assert.AreEqual(stateBeforePause, grid.CurrenteStateName);
    }

    [Test]
    public void CallDeleteEventBeforeDeletingPhase()
    {
        var wasCalled = false;

        grid.OnDeleteEvent += (gridobj, blocks, chains) => wasCalled = true;

        Assert.IsTrue(grid.AddGroup(group));
        Assert.IsTrue(group.Location.Equals(setting.BlockSpawnPoint));
        grid.FixGroup();
        Assert.IsTrue(grid.DropBlocks());

        grid.SetState(GridStates.Dropped);
        Assert.IsNotNull(grid[3, 1]);
        Assert.IsNotNull(grid[3, 0]);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleting);
        Assert.IsTrue(wasCalled);
    }

    [Test]
    public void itWillAddOnDeleteEventListener()
    {
        var iOnDeleteEventListener = Substitute.For<IOnDeleteEventListener>();
        grid.AddOnDeleteEventListener(iOnDeleteEventListener);

        var blockOne = Substitute.For<IBlock>();
        blockOne.Number.Returns(1);
        var blockTwo = Substitute.For<IBlock>();
        blockTwo.Number.Returns(6);

        grid[3, 1] = blockOne;
        grid[3, 0] = blockTwo;

        grid.SetState(GridStates.Dropped);
        Assert.IsNotNull(grid[3, 1]);
        Assert.IsNotNull(grid[3, 0]);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleting);

        iOnDeleteEventListener.Received().OnDeleteEvent(Arg.Any<IGrid>(), Arg.Any <List<IBlock>>(), Arg.Any<int>());
    }

    [Test]
    public void itWillRemoveEventListener()
    {
        var iOnDeleteEventListener = Substitute.For<IOnDeleteEventListener>();
        grid.AddOnDeleteEventListener(iOnDeleteEventListener);
        grid.RemoveOnDeleteEventListener(iOnDeleteEventListener);

        var blockOne = Substitute.For<IBlock>();
        blockOne.Number.Returns(1);
        var blockTwo = Substitute.For<IBlock>();
        blockTwo.Number.Returns(6);

        grid[3, 1] = blockOne;
        grid[3, 0] = blockTwo;

        grid.SetState(GridStates.Dropped);
        Assert.IsNotNull(grid[3, 1]);
        Assert.IsNotNull(grid[3, 0]);
        grid.OnUpdate();
        Assert.IsTrue(grid.CurrenteStateName == GridStates.Deleting);

        iOnDeleteEventListener.DidNotReceive().OnDeleteEvent(Arg.Any<IGrid>(), Arg.Any<List<IBlock>>(), Arg.Any<int>());
    }

    [Test]
    public void CantChangeStateWhenItsGameOver()
    {
        grid.GameOver();
        Assert.AreEqual(GridStates.GameOver, grid.CurrenteStateName);

        grid.SetState(GridStates.ReadyForNextGroup);
        Assert.AreEqual(GridStates.GameOver, grid.CurrenteStateName);
    }
}
