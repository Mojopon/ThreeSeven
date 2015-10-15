using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GameTest  
{

    Game game;
    ICameraManager cameraManager;
    IBackgroundFactory backgroundFactory;
    IGridFactory gridFactory;
    IGroupFactory groupFactory;

    ISetting testSetting;

    [SetUp]
    public void Init()
    {
        testSetting = TestSetting.Get();
        game = new Game(testSetting);
        cameraManager = Substitute.For<ICameraManager>();
        backgroundFactory = Substitute.For<IBackgroundFactory>();
        gridFactory = Substitute.For<IGridFactory>();
        groupFactory = Substitute.For<IGroupFactory>();

        game.CameraManager = cameraManager;
        game.BackgroundFactory = backgroundFactory;
        game.GridFactory = gridFactory;
        game.GroupFactory = groupFactory;
    }

    [Test]
    public void GameUsesSettingsToCreateWithFactory()
    {
        game.InitializeGrid();

        backgroundFactory.Received().Create(game);
        gridFactory.Received().Create();

        cameraManager.Received().SetCameraPosition(Arg.Any<Vector3>());
    }

    [Test]
    public void ShouldPauseGameAndGrid()
    {
        IGrid gridMock = Substitute.For<IGrid>();

        Game game = new Game(gridMock, testSetting);

        game.OnUpdate();
        gridMock.Received().OnUpdate();
        gridMock.ClearReceivedCalls();

        game.Pause();
        game.OnUpdate();
        gridMock.DidNotReceive().OnUpdate();
    }

}
