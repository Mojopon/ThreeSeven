using UnityEngine;
using System.Collections;

public class CPUManager : ICPUManager 
{
    private IGrid _grid;
    private ICPUBehaviour _behaviour;
    public CPUManager(IGrid grid)
    {
        _grid = grid;
        _behaviour = new NullBehaviour();
    }

    public void ChangeCPUMode(CPUMode mode)
    {
        switch (mode)
        {
            case CPUMode.None:
                _behaviour = new NullBehaviour();
                break;
            case CPUMode.Easy:
                _behaviour = new RandomMovementBehaviour(_grid);
                break;
        }
    }

    public void OnUpdate()
    {
        _behaviour.DoAction();
    }
}
