using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GameManagerSystemTest 
{
    private GameManagerSystem gameManagerSystem;

    [SetUp]
    public void Initialize()
    {
        gameManagerSystem = new GameManagerSystem();
    }

    [Test]
    public void ShouldUpdateGame()
    {
        IGame game = Substitute.For<IGame>();
        gameManagerSystem.AddGame(game);

        gameManagerSystem.OnUpdate();

        game.Received().OnUpdate();
    }

    [Test]
    public void ShouldUpdateEveryGames()
    {
        IGame game = Substitute.For<IGame>();
        gameManagerSystem.AddGame(game);

        IGame game2 = Substitute.For<IGame>();
        gameManagerSystem.AddGame(game2);

        gameManagerSystem.OnUpdate();

        game.Received().OnUpdate();
        game2.Received().OnUpdate();
    }
	
    [Test]
    public void ShouldPauseGames()
    {
        IGame game = Substitute.For<IGame>();
        gameManagerSystem.AddGame(game);

        IGame game2 = Substitute.For<IGame>();
        gameManagerSystem.AddGame(game2);

        gameManagerSystem.Pause();

        game.Received().Pause();
        game2.Received().Pause();
    }
}
