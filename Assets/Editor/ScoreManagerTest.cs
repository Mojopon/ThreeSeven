using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class ScoreManagerTest {

    IScoreManager scoreManager;
    IGameText scoreText;

    [SetUp]
    public void Init()
    {
        scoreManager = new ScoreManager();
        scoreText = Substitute.For<IGameText>();
    }

	[Test]
    public void ShouldUpdateScoreText()
    {
        scoreManager.AttachScoreText(scoreText);

        List<IBlock> blockList = CreateBlockListMock();

        scoreManager.OnDeleteBlocks(blockList, 1);

        scoreText.Received().UpdateText(Arg.Any<string>());
    }

    List<IBlock> CreateBlockListMock()
    {
        IBlock blockOne = Substitute.For<IBlock>();
        IBlock blockTwo = Substitute.For<IBlock>();
        IBlock blockThree = Substitute.For<IBlock>();

        List<IBlock> blockList = new List<IBlock>();
        blockList.Add(blockOne);
        blockList.Add(blockTwo);
        blockList.Add(blockThree);

        return blockList;
    }
}
