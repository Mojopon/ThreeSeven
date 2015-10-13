using UnityEngine;
using System.Collections.Generic;

public class GameEntity : MonoBehaviour, IControllable {

    public GameDirector[] gameDirectors;

    List<Game> games;
    IControllable currentControl;

    GameObject playerGame;

    List<IGroupPattern> groupPatterns;

    void Awake()
    {
        games = new List<Game>();
        currentControl = NullControl.Instance;

        CreateNewGame();
    }

    void CreateNewGame()
    {
        foreach (GameDirector gameDirector in gameDirectors)
        {
            AddGame(gameDirector.Construct());
        }
    }

    void AddGame(Game game)
    {
        if (currentControl == NullControl.Instance)
        {
            currentControl = game;
        }
        games.Add(game);
    }

    void Update()
    {
        foreach (Game game in games)
        {
            game.OnUpdate();
        }
    }

    #region IControllable Method Group

    public void OnArrowKeyInput(Direction direction)
    {
        currentControl.OnArrowKeyInput(direction);
    }

    public void OnJumpKeyInput()
    {
        foreach (Game game in games)
        {
            game.OnJumpKeyInput();
        }
    }

    #endregion
}
