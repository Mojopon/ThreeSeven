using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameServer : IGameServer {

    protected List<IGrid> _grids;
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

    protected List<IGrid> _playersInGame;
    protected bool _gameIsRunning = false;
    public virtual void StartNewGame()
    {
        _playersInGame = new List<IGrid>();

        foreach(IGrid grid in _grids)
        {
            grid.NewGame();
            AddPlayerToPlayersInGame(grid);
        }

        _gameIsRunning = true;
    }

    public virtual void FinishGame()
    {
        _gameIsRunning = false;

        foreach (IGrid grid in _grids)
        {
            grid.GameOver();
            RemoveGameOverPlayer(grid);
        }
    }

    protected void AddPlayerToPlayersInGame(IGrid grid)
    {
        _playersInGame.Add(grid);
        grid.OnGameOverEvent += new OnGameOverEventHandler(RemoveGameOverPlayer);
    }

    protected void RemoveGameOverPlayer(IGrid grid)
    {
        _playersInGame.Remove(grid);
        grid.OnGameOverEvent -= new OnGameOverEventHandler(RemoveGameOverPlayer);

        if (_playersInGame.Count <= 1 && _gameIsRunning)
        {
            FinishGame();
        }
    }
}
