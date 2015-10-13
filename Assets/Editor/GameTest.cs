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
        game.StartThreeSeven();

        backgroundFactory.Received().Create(game);
        gridFactory.Received().Create();

        cameraManager.Received().SetCameraPosition(Arg.Any<Vector3>());
    }

}
