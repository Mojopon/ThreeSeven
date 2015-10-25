using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameServer : IGameServer {

    private List<IGrid> _grids;
	public GameServer()
    {
        _grids = new List<IGrid>();
    }

    public void Register(IGrid grid)
    {
        if(_grids.Contains(grid))
        {
            Debug.Log("Tried to add an existed grid!");
            return;
        }

        _grids.Add(grid);
    }

    public void StartNewGame()
    {
        foreach(IGrid grid in _grids)
        {
            grid.NewGame();
        }
    }
}
