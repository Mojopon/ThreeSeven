using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GameTextManagerTest  {

    IGameTextManager gameTextManager;
    GameTextComponent[] textComponents;

    IGameText levelText;
    IGameText scoreText;

    [SetUp]
    public void Init()
    {
        /*levelText = Substitute.For<IGameText>();
        scoreText = Substitute.For<IGameText>();

        textComponents = new GameTextComponent[] 
        {
            new GameTextComponent() { gameText = levelText, gameTextType = GameTextType.LevelText },
            new GameTextComponent() { gameText = scoreText, gameTextType = GameTextType.ScoreText },
        };

        gameTextManager = new GameTextManager(textComponents);*/
    }

	/*[Test]
    public void ShouldReturnRequestedGameText()
    {
        IGameText requestedText = gameTextManager.GetGameText(GameTextType.LevelText);
        Assert.AreEqual(requestedText, levelText);
    }*/
}
