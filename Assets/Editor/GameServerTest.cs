using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GameServerTest : GridTestFixture
{
    IGameServer gameServer;

    IGrid gridOne;
    IGrid gridTwo;

    [SetUp]
    public void Initialize()
    {
        gameServer = new GameServer();

        gridOne = Substitute.For<IGrid>();
        gridTwo = Substitute.For<IGrid>();
    }

    [Test]
    public void ShouldAddOnGameOverEventWhenStartGames()
    {
        gameServer.Register(gridOne);
        gameServer.Register(gridTwo);

        gameServer.StartNewGame();

        gridOne.Received().OnGameOverEvent += Arg.Any<OnGameOverEventHandler>();
        gridTwo.Received().OnGameOverEvent += Arg.Any<OnGameOverEventHandler>();
    }

    [Test]
    public void ShouldRemoveOnGameOverEventWhenFinish()
    {
        gameServer.Register(gridOne);
        gameServer.Register(gridTwo);

        gameServer.StartNewGame();

        gameServer.FinishGame();

        gridOne.Received().OnGameOverEvent -= Arg.Any<OnGameOverEventHandler>();
        gridTwo.Received().OnGameOverEvent -= Arg.Any<OnGameOverEventHandler>();
    }

    [Test]
    public void ShouldCallGameOverForPlayersInGame()
    {
        gameServer.Register(gridOne);
        gameServer.Register(gridTwo);

        gameServer.StartNewGame();

        gameServer.FinishGame();

        gridOne.Received().GameOver();
        gridTwo.Received().GameOver();
    }

    [Test]
    public void ShouldStartAllGames()
    {
        gameServer.Register(gridOne);
        gameServer.Register(gridTwo);

        gameServer.StartNewGame();

        gridOne.Received().NewGame();   
        gridTwo.Received().NewGame();
    }
}
