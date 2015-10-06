using UnityEngine;
using System.Collections;

public class OnControlGroupState : IGridState {

    public GridStates StateEnum { get { return GridStates.OnControlGroup; } }

    private IGrid _grid;
    public OnControlGroupState(IGrid grid)
    {
        _grid = grid;
    }

    public void OnUpdate()
    {
        if (!_grid.ControllingGroup)
        {
            _grid.SetState(GridStates.OnFix);
        }
    }
}
