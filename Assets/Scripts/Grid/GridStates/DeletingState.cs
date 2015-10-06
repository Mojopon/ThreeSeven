using UnityEngine;
using System.Collections;

public class DeletingState : IGridState {

    public GridStates StateEnum { get { return GridStates.Deleting; } }
    IGrid _grid;

    public DeletingState(IGrid grid)
    {
        _grid = grid;
    }


    public void OnUpdate()
    {
        if (!_grid.ProcessDeleting())
        {
            _grid.SetState(GridStates.Deleted);
        }
    }
}
