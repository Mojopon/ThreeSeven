using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class _TestSettingInitializer  
{
    SettingManager settingManager;

    [SetUp]
    public void Init()
    {
        settingManager = SettingManager.Instance;
        settingManager.Mode = SettingMode.Test;
    }

    [Test]
    public void SettingManagerShouldBeSetToTestMode()
    {
        Assert.IsTrue(SettingManager.Instance.Mode == SettingMode.Test);
    }

}
