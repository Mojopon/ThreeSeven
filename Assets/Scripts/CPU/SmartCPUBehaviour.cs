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
        if (movements == null) return;

        ProcessMovement();
    }

    private List<Direction> movements;
    void OnGroupAddEvent(IGrid grid, IGroup group)
    {
        movements = _outputter.Output();
    }

    private int currentRotation;
    private Coord currentLocation;
    public void ProcessMovement()
    {
        currentRotation = _grid.CurrentGroup.CurrentRotatePatternNumber;
        currentLocation = _grid.CurrentGroup.Location;

        if(movements.Count == 0)
        {
            _grid.OnArrowKeyInput(Direction.Down);
        }
        else
        {
            _grid.OnArrowKeyInput(movements[0]);
            if (currentRotation != _grid.CurrentGroup.RotationPatternNumber ||
               currentLocation != _grid.CurrentGroup.Location)
            {
                movements.Remove(movements[0]);
            }
        }
    }
}
