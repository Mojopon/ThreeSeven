using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IGameManager {

    public GameDirector[] gameDirectors;
    public GameMode gameMode = GameMode.SinglePlayerMode;

    private GameManagerSystem gameManagerSystem;

    void Awake()
    {
        gameManagerSystem = new GameManagerSystem();

        CreateNewGame();
    }

    void CreateNewGame()
    {
        foreach (GameDirector gameDirector in gameDirectors)
        {
            AddGame(gameDirector.Construct());
        }
    }

    public void AddGame(IGame game)
    {
        gameManagerSystem.AddGame(game);
    }

    void Update()
    {
        gameManagerSystem.OnUpdate();
    }
}
