using UnityEngine;
using System.Collections;

public class DroppedState : IGridState {

    public GridStates StateEnum { get { return GridStates.Dropped; } }
    IGrid _grid;

    public DroppedState(IGrid grid)
    {
        _grid = grid;
    }

    public void OnUpdate()
    {
        if (_grid.StartDeleting())
        {
            _grid.SetState(GridStates.Deleting);
        }
        else
        {
            _grid.SetState(GridStates.ReadyForNextGroup);
        }
    }
}
