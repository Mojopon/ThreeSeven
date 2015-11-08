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
        var patterns = groupPattern.Patterns;
        int initialRotatePatternNumber = 3;

        rotatePatternManager = new RotatePatternManager(groupPattern.Patterns, initialRotatePatternNumber);
        Assert.AreEqual(initialRotatePatternNumber, rotatePatternManager.CurrentRotatePatternNumber);

        var currentPattern = rotatePatternManager.GetCurrentPattern();
        for (int i = 0; i < patterns[initialRotatePatternNumber].Length; i++)
        {
            Assert.AreEqual(currentPattern[i], patterns[initialRotatePatternNumber][i]);
        }
    }

    [Test]
    public void NumberWillBeZeroWhenSetInvalidValue()
    {
        var patterns = groupPattern.Patterns;
        int initialRotatePatternNumber = 4;

        rotatePatternManager = new RotatePatternManager(groupPattern.Patterns, initialRotatePatternNumber);
        Assert.AreEqual(0, rotatePatternManager.CurrentRotatePatternNumber);

        var currentPattern = rotatePatternManager.GetCurrentPattern();
        for (int i = 0; i < patterns[0].Length; i++)
        {
            Assert.AreEqual(currentPattern[i], patterns[0][i]);
        }
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
