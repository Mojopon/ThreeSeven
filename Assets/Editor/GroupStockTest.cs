using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GroupStockTest : ThreeSevenTestFixsture
{
    new IGroupFactory groupFactory;
    IGroupStock groupStock;
    Vector3[] stockPositions = new Vector3[] 
    {
        new Vector3(3, 3, 0),
        new Vector3(2, 2, 0),
        new Vector3(1, 1, 0),
    };

    [SetUp]
    public void Init()
    {
        groupFactory = Substitute.For<IGroupFactory>();
        groupStock = new GroupStock(groupFactory);
        groupStock.StockPositions = stockPositions;
    }
	
    [Test]
    public void FactoryShouldReceiveCreateWhenPrepare()
    {
        groupStock.Prepare(setting);
        groupFactory.Received(3).Create(setting);
    }

}
