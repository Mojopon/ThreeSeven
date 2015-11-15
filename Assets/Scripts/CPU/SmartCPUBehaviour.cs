using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;
using System.Collections;
using System.Threading;

public class SmartCPUBehaviour : ICPUBehaviour
{
    private IGrid _grid;
    private IGridSimulator _gridSimulator;
    private OutputBestMovement _outputter;
    private float timeBetweenActions;
    private float timeBeforeAction;
    public SmartCPUBehaviour(IGrid grid, ISetting setting, CPUMode difficulty)
    {
        _grid = grid;
        _gridSimulator = new GridSimulator(grid, setting);
        _outputter = new OutputBestMovement(_gridSimulator);
        _grid.OnGroupAdd += new OnGroupAddEventHandler(OnGroupAddEvent);

        Debug.Log("difficulty " + difficulty + "set");
        switch(difficulty)
        {
            case CPUMode.Easy:
                timeBeforeAction = 0.5f;
                timeBetweenActions = 0.5f;
                break;
            case CPUMode.Normal:
                timeBeforeAction = 0.5f;
                timeBetweenActions = 0.3f;
                break;
            case CPUMode.Hard:
                timeBeforeAction = 0.2f;
                timeBetweenActions = 0.1f;
                break;
            case CPUMode.Kusotuyo:
                timeBeforeAction = 0f;
                timeBetweenActions = 0f;
                break;
        }
    }

    public void DoAction()
    {
        if (movements == null) return;

        ProcessMovement();
    }

    private List<Direction> movements;
    void OnGroupAddEvent(IGrid grid, IGroup group)
    {
        var coroutine = Observable.FromCoroutine(GetOutPutCoroutine)
                                  .Subscribe(_ => onWaitingOutput = false);
    }

    private bool onWaitingOutput = false;
    private IEnumerator GetOutPutCoroutine()
    {
        onWaitingOutput = true;
        yield return Observable.Start(() => {
            Thread.Sleep((int)(timeBeforeAction * 1000));
            return GetOutPut();
        }).StartAsCoroutine((list) => movements = list);
    }

    private List<Direction> GetOutPut()
    {
        return _outputter.Output();
    }

    private float nextMoveTime;
    private int currentRotation;
    private Coord currentLocation;
    public void ProcessMovement()
    {
        if (onWaitingOutput || movements == null || Time.time < nextMoveTime) return;

        currentRotation = _grid.CurrentGroup.CurrentRotatePatternNumber;
        currentLocation = _grid.CurrentGroup.Location;

        if(movements.Count == 0)
        {
            _grid.OnJumpKeyInput();
        }
        else
        {
            _grid.OnArrowKeyInput(movements[0]);
            if (currentRotation != _grid.CurrentGroup.RotationPatternNumber ||
               currentLocation != _grid.CurrentGroup.Location)
            {
                // movement succeed;
                movements.Remove(movements[0]);
                nextMoveTime = Time.time + timeBetweenActions;
            }
        }
    }
}
