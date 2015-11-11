using UnityEngine;
using System.Collections;
using System;

public class SmartCPUBehaviour : ICPUBehaviour
{
    private IGridSimulator _gridSimulator;
    private OutputBestMovement _outputter;
    public SmartCPUBehaviour(IGrid grid, ISetting setting)
    {
        _gridSimulator = new GridSimulator(grid, setting);
        _outputter = new OutputBestMovement(_gridSimulator);
    }

    public void DoAction()
    {
        throw new NotImplementedException();
    }
}
