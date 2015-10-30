using UnityEngine;
using System.Collections;

public class ReadyForNextGroupState : IGridState {

    public GridStates StateEnum { get { return GridStates.ReadyForNextGroup; } }
    ISetting _setting;
    IGrid _grid;
    IGroupFactory _groupFactory;
    OnDeleteEndEventHandler _onDeleteEndEvent;

    public ReadyForNextGroupState(ISetting setting, IGrid grid, IGroupFactory groupFactory, OnDeleteEndEventHandler onDeleteEndEvent)
    {
        _setting = setting;
        _grid = grid;
        _groupFactory = groupFactory;
        _onDeleteEndEvent = onDeleteEndEvent;
    }

    public void OnUpdate()
    {
        if(_onDeleteEndEvent != null) _onDeleteEndEvent(_grid);

        if(_grid.CurrenteStateName == GridStates.GameOver)
        {
            return;
        }

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
