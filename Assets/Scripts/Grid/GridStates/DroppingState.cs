using UnityEngine;
using System.Collections;

public class DroppingState : IGridState 
{

    public GridStates StateEnum { get { return GridStates.Dropping; } }
    IGrid _grid;

    public DroppingState(IGrid grid)
    {
        _grid = grid;
    }

    public void OnUpdate()
    {
        if (!_grid.MoveBlocks())
        {
            _grid.SetState(GridStates.Dropped);
        }
    }
}
