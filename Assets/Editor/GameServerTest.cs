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
    public void ShouldAddOnGameOverEvent()
    {
        gameServer.Register(gridOne);
        gameServer.Register(gridTwo);

        gridOne.Received().OnGameOverEvent += Arg.Any<OnGameOver>();
        gridTwo.Received().OnGameOverEvent += Arg.Any<OnGameOver>();
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
