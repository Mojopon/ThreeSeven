using UnityEngine;
using System.Collections.Generic;

public class GameManagerSystem : IGameManager, IUpdatable, IPauseEvent {

    private List<IGame> games;

    public GameManagerSystem() 
    {
        games = new List<IGame>();

        InputManager.OnPauseKeyPressed += new InputManager.PauseKeyEvent(Pause);
    }


    public void AddGame(IGame game)
    {
        games.Add(game);
    }

    public void OnUpdate()
    {
        foreach (IGame game in games)
        {
            game.OnUpdate();
        }
    }

    public void Pause()
    {
        foreach (IGame game in games)
        {
            game.Pause();
        }
    }
}
