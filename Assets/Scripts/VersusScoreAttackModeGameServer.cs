using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class VersusScoreAttackModeGameServer : GameServer, IOnDeleteEventListener
{
    private int goalScore;
    public VersusScoreAttackModeGameServer()
    {
        goalScore = 500;
    }

    public void OnDeleteEvent(IGrid grid, List<IBlock> blocksToDelete, int chains)
    {
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
            AddOnDeleteEventToTheGrid(grid);
        }
    }

    public override void FinishGame()
    {
        base.FinishGame();

        foreach (IGrid grid in _grids)
        {
            RemoveOnDeleteEventFromTheGrid(grid);
        }
    }

    void AddOnDeleteEventToTheGrid(IGrid grid)
    {
        grid.AddOnDeleteEventListener(this);
    }

    void RemoveOnDeleteEventFromTheGrid(IGrid grid)
    {
        grid.RemoveOnDeleteEventListener(this);
    }
}
