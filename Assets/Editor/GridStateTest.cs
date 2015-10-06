using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GridStateTest : ThreeSevenTestFixsture {

    IGrid gridMock;
    IGroupFactory groupFactoryMock;

    [SetUp]
    public void Init()
    {
        gridMock = Substitute.For<IGrid>();
        groupFactoryMock = Substitute.For<IGroupFactory>();
    }

    [Test]
    public void DeletedShouldShiftToDroppingOnUpdate()
    {
        IGridState deletedState = new DeletedState(gridMock);

        deletedState.OnUpdate();
        gridMock.Received().DropBlocks();
        gridMock.Received().SetState(GridStates.Dropping);
    }

    [Test]
    public void DroppingShouldShiftToDroppedOnUpdateWhenNothingToMove()
    {
        IGridState droppingState = new DroppingState(gridMock);
        gridMock.MoveBlocks().Returns(true);

        droppingState.OnUpdate();
        gridMock.DidNotReceive().SetState(GridStates.Dropped);

        gridMock.MoveBlocks().Returns(false);
        droppingState.OnUpdate();
        gridMock.Received().SetState(GridStates.Dropped);
    }

    [Test]
    public void DroppedShouldShiftToReadyForNextGroupWhenNothingToDelete()
    {
        IGridState droppedState = new DroppedState(gridMock);
        gridMock.StartDeleting().Returns(false);

        droppedState.OnUpdate();
        gridMock.Received().SetState(GridStates.ReadyForNextGroup);
    }

    [Test]
    public void DroppedShouldShiftToDeletingWhenTheresABlockToDelete()
    {
        IGridState droppedState = new DroppedState(gridMock);
        gridMock.StartDeleting().Returns(true);

        droppedState.OnUpdate();
        gridMock.Received().SetState(GridStates.Deleting);
    }

    [Test]
    public void DeletingShouldShiftToDeletedWhenBlocksCompleteMoving()
    {
        IGridState deletingState = new DeletingState(gridMock);
        gridMock.ProcessDeleting().Returns(true);

        deletingState.OnUpdate();
        gridMock.DidNotReceive().SetState(GridStates.Deleted);

        gridMock.ProcessDeleting().Returns(false);

        deletingState.OnUpdate();
        gridMock.Received().SetState(GridStates.Deleted);
    }

    [Test]
    public void OnControlGroupShouldShiftToOnFixWhenGroupCantMove()
    {
        IGridState onControlGroupState = new OnControlGroupState(gridMock);
        gridMock.ControllingGroup.Returns(true);

        onControlGroupState.OnUpdate();
        gridMock.DidNotReceive().SetState(GridStates.OnFix);

        gridMock.ControllingGroup.Returns(false);

        onControlGroupState.OnUpdate();
        gridMock.Received().SetState(GridStates.OnFix);
    }

    [Test]
    public void OnFixGroupdShouldShiftToDroppingState()
    {
        IGridState onFixState = new OnFixState(gridMock);

        onFixState.OnUpdate();
        gridMock.Received().SetState(GridStates.Dropping);
    }

    [Test]
    public void ReadyForNextGroupShouldCreateGroupToAdd()
    {
        IGridState readyFoNextGroupState = new ReadyForNextGroupState(setting, gridMock, groupFactoryMock);
        gridMock.AddGroup(Arg.Any<IGroup>()).Returns(true);

        readyFoNextGroupState.OnUpdate();
        groupFactoryMock.Received().Create(setting);
        gridMock.Received().SetState(GridStates.OnControlGroup);

    }

    [Test]
    public void ShouldBeGameOverWhenCantAddGroup()
    {
        IGridState readyFoNextGroupState = new ReadyForNextGroupState(setting, gridMock, groupFactoryMock);
        gridMock.AddGroup(Arg.Any<IGroup>()).Returns(false);

        readyFoNextGroupState.OnUpdate();
        groupFactoryMock.Received().Create(setting);
        gridMock.Received().GameOver();
    }
}
