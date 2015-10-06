using UnityEngine;
using System.Collections;

public class ReadyForNextGroupState : IGridState {

    public GridStates StateEnum { get { return GridStates.ReadyForNextGroup; } }
    ISetting _setting;
    IGrid _grid;
    IGroupFactory _groupFactory;

    public ReadyForNextGroupState(ISetting setting, IGrid grid, IGroupFactory groupFactory)
    {
        _setting = setting;
        _grid = grid;
        _groupFactory = groupFactory;
    }

    public void OnUpdate()
    {
        IGroup group = _groupFactory.Create(_setting);
        _grid.ResetChains();
        if (_grid.AddGroup(group))
        {
            _grid.SetState(GridStates.OnControlGroup);
        }
        else 
        {
            _grid.GameOver();
        }
    }
}
