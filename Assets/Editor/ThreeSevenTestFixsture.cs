using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class ThreeSevenTestFixsture  {

    protected IGroupFactory groupFactory;
    protected IBlockFactory blockFactory;
    protected IBlockViewSpawner blockViewSpawner;

    protected ISetting setting;

    [SetUp]
    public void Init()
    {
        //blockViewSpawner = Substitute.For<IBlockViewSpawner>();
        blockFactory = new BlockFactory(blockViewSpawner);
        groupFactory = new GroupFactory(blockFactory);

        setting = SettingManager.Instance.GetSetting();
    }
}
