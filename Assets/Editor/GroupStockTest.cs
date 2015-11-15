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
    StockDisplayConfig[] stockPositions = new StockDisplayConfig[] 
    {
        new StockDisplayConfig() {position =  new Vector3(3, 3, 0) },
        new StockDisplayConfig() {position =  new Vector3(2, 2, 0) },
        new StockDisplayConfig() {position =  new Vector3(1, 1, 0) },
    };

    [SetUp]
    public void Init()
    {
        groupFactory = Substitute.For<IGroupFactory>();
        groupStock = new GroupStock(groupFactory);
        groupStock.StockDisplayConfig = stockPositions;
    }
	
    [Test]
    public void FactoryShouldReceiveCreateWhenPrepare()
    {
        groupStock.Prepare(setting);
        groupFactory.Received(3).Create(setting);
    }

}
