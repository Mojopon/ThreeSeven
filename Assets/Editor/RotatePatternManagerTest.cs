using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class RotatePatternManagerTest : GridTestFixture
{
    RotatePatternManager rotatePatternManager;

    [SetUp]
    public void CreateRotatePatternManager()
    {
        rotatePatternManager = new RotatePatternManager(groupPattern.Patterns);
    }

    [Test]
    public void ItCanSetInitialRotatePatternNumber()
    {
        rotatePatternManager = new RotatePatternManager(groupPattern.Patterns, 3);
        Assert.AreEqual(3, rotatePatternManager.CurrentRotatePatternNumber);
    }

    [Test]
    public void NumberWillBeZeroWhenSetInvalidValue()
    {
        rotatePatternManager = new RotatePatternManager(groupPattern.Patterns, 4);
        Assert.AreEqual(0, rotatePatternManager.CurrentRotatePatternNumber);
    }

    [Test]
    public void ShouldReturnCurrentGroupPattern()
    {
        var patterns = groupPattern.Patterns;
        int currentRotationNumber = 0;

        var currentPattern = rotatePatternManager.GetCurrentPattern();
        for(int i = 0; i < patterns[currentRotationNumber].Length; i++)
        {
            Assert.AreEqual(currentPattern[i], patterns[currentRotationNumber][i]);
        }

        for (int j = 0; j < 4; j++)
        {
            currentRotationNumber++;
            if (currentRotationNumber == 4) currentRotationNumber = 0;
            currentPattern = rotatePatternManager.GetRotatedPattern(RotateDirection.Clockwise);

            for (int i = 0; i < patterns[currentRotationNumber].Length; i++)
            {
                Assert.AreEqual(currentPattern[i], patterns[currentRotationNumber][i]);
            }
        }
    }
}
