using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class VersusScoreAttackModeGameServer : GameServer
{
    private int _goalScore;
    public VersusScoreAttackModeGameServer()
    {
        _goalScore = 10000;
    }

    public VersusScoreAttackModeGameServer(int goalScore)
    {
        _goalScore = goalScore;
    }

    public void OnDeleteEvent(IGrid grid, List<IBlock> blocksToDelete, int chains)
    {
        if (grid.CurrenteStateName == GridStates.GameOver) return;

        if(grid.CurrentScore >= _goalScore)
        {
            FinishGame();
        }
    }

    public override void StartNewGame()
    {
        base.StartNewGame();

        foreach(IGrid grid in _grids)
        {
            grid.OnDeleteEndEvent += new OnDeleteEndEventHandler(OnDeleteEndEvent);    
        }
    }

    public override void FinishGame()
    {
        base.FinishGame();

        foreach (IGrid grid in _grids)
        {
            grid.OnDeleteEndEvent -= new OnDeleteEndEventHandler(OnDeleteEndEvent);
        }
    }

    void OnDeleteEndEvent(IGrid grid)
    {
        if (grid.CurrentScore >= _goalScore)
        {
            FinishGame();
        }
    }
}
