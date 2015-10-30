using UnityEngine;
using System.Collections.Generic;

public class GameManagerSystem : IGameManager, IUpdatable, IPauseEvent {

    private List<IGame> _games;
    private IGameServer _gameServer;

    public GameManagerSystem() 
    {
        _games = new List<IGame>();
        //_gameServer = new GameServer();
        _gameServer = new VersusScoreAttackModeGameServer();

        InputManager.OnPauseKeyPressed += new InputManager.PauseKeyEvent(Pause);
    }


    public void AddGame(IGame game)
    {
        _games.Add(game);
        game.RegisterGameToTheGameServer(_gameServer);
    }

    public void OnUpdate()
    {
        foreach (IGame game in _games)
        {
            game.OnUpdate();
        }
    }

    public void Pause()
    {
        foreach (IGame game in _games)
        {
            game.Pause();
        }
    }
}
