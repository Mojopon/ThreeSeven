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
    public void CreateGrids()
    {
        gridOne = Substitute.For<IGrid>();
        gridTwo = Substitute.For<IGrid>();
    }

    [Test]
    public void ShouldEndGameWhenOnlyOnePlayerAlive()
    {

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
