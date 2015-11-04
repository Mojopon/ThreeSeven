using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class VersusScoreAttackModeServerTest: GridTestFixture
{
    IGameServer gameServer;

    IGrid gridOne;
    IGrid gridTwo;

    [SetUp]
    public void CanSetGoalScoreWhenInitialize()
    {
        gameServer = new VersusScoreAttackModeGameServer(100);

        gridOne = Substitute.For<IGrid>();
        gridTwo = Substitute.For<IGrid>();
    }

    [Test]
    public void ShouldAddOnDeleteEndEventWhenStartGames()
    {
        gameServer.Register(gridOne);
        gameServer.Register(gridTwo);

        gameServer.StartNewGame();

        gridOne.Received().OnDeleteEndEvent += Arg.Any<OnDeleteEndEventHandler>();
        gridTwo.Received().OnDeleteEndEvent += Arg.Any<OnDeleteEndEventHandler>();
    }

    [Test]
    public void ShouldRemoveOnDeleteEndEventWhenGameEnds()
    {
        gameServer.Register(gridOne);
        gameServer.Register(gridTwo);

        gameServer.StartNewGame();

        gridOne.Received().OnDeleteEndEvent += Arg.Any<OnDeleteEndEventHandler>();
        gridTwo.Received().OnDeleteEndEvent += Arg.Any<OnDeleteEndEventHandler>();

        gameServer.FinishGame();

        gridOne.Received().OnDeleteEndEvent -= Arg.Any<OnDeleteEndEventHandler>();
        gridTwo.Received().OnDeleteEndEvent -= Arg.Any<OnDeleteEndEventHandler>();
    }
}