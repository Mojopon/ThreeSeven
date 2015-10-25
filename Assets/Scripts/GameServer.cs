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

    private List<IGrid> _playersInGame;
    public void StartNewGame()
    {
        _playersInGame = new List<IGrid>();

        foreach(IGrid grid in _grids)
        {
            grid.NewGame();
            AddPlayerToPlayersInGame(grid);
        }
    }

    public void FinishGame()
    {
        foreach (IGrid grid in _grids)
        {
            grid.GameOver();
            grid.OnGameOverEvent -= new OnGameOverEventHandler(RemoveGameOverPlayer);
        }
    }

    void AddPlayerToPlayersInGame(IGrid grid)
    {
        _playersInGame.Add(grid);
        grid.OnGameOverEvent += new OnGameOverEventHandler(RemoveGameOverPlayer);
    }

    void RemoveGameOverPlayer(IGrid grid)
    {
        _playersInGame.Remove(grid);
        if(_playersInGame.Count <= 1)
        {
            FinishGame();
        }
    }
}
