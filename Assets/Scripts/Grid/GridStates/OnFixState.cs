using UnityEngine;
using System.Collections;

public class OnFixState : IGridState 
{
    public GridStates StateEnum { get { return GridStates.OnFix; } }

    private IGrid _grid;

    public OnFixState(IGrid grid)
    {
        _grid = grid;
    }

    public void OnUpdate()
    {
        _grid.FixGroup();
        _grid.DropBlocks();
        _grid.SetState(GridStates.Dropping);
    }
}
