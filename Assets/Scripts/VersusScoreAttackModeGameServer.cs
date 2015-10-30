using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class VersusScoreAttackModeGameServer : GameServer
{
    private int goalScore;
    public VersusScoreAttackModeGameServer()
    {
        goalScore = 500;
    }

    public void OnDeleteEvent(IGrid grid, List<IBlock> blocksToDelete, int chains)
    {
        if (grid.CurrenteStateName == GridStates.GameOver) return;

        if(grid.CurrentScore >= goalScore)
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
        if (grid.CurrentScore >= goalScore)
        {
            FinishGame();
        }
    }
}
