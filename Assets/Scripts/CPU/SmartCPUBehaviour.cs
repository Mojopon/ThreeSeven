using UnityEngine;
using System.Collections.Generic;
using System;

public class SmartCPUBehaviour : ICPUBehaviour
{
    private IGrid _grid;
    private IGridSimulator _gridSimulator;
    private OutputBestMovement _outputter;
    public SmartCPUBehaviour(IGrid grid, ISetting setting)
    {
        _grid = grid;
        _gridSimulator = new GridSimulator(grid, setting);
        _outputter = new OutputBestMovement(_gridSimulator);
        _grid.OnGroupAdd += new OnGroupAddEventHandler(OnGroupAddEvent);
    }

    public void DoAction()
    {
        ProcessMovement();
    }

    private List<Direction> movements = new List<Direction>();
    void OnGroupAddEvent(IGrid grid, IGroup group)
    {
        movements = _outputter.Output();
    }

    public void ProcessMovement()
    {
        if(movements.Count == 0)
        {
            _grid.OnArrowKeyInput(Direction.Down);
        }
        else
        {
            _grid.OnArrowKeyInput(movements[0]);
            movements.Remove(movements[0]);
        }
    }
}
