using UnityEngine;
using System.Collections;

public class DeletedState : IGridState {

    public GridStates StateEnum { get { return GridStates.Deleted; } }
    IGrid _grid;

    public DeletedState(IGrid grid)
    {
        _grid = grid;
    }

    public void OnUpdate()
    {
        _grid.DropBlocks();
        _grid.SetState(GridStates.Dropping);
    }
}
