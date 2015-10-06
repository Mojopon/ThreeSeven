using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class BlockTypeHelperTest  {

    [Test]
	public void ShouldReturnRandomBlockType() 
    {
        for (int i = 0; i < 100; i++)
        {
            int randomNumber = (int)BlockTypeHelper.GetRandom();
            Assert.IsTrue(randomNumber >= 0 && randomNumber < (int)BlockType.AvailableBlocks);
        }
    }
}
