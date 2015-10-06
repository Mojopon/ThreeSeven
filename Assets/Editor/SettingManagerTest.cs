using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class SettingManagerTest  
{
    [Test]
    public void ShouldReturnDevelopmentSetting()
    {
        SettingManager settingManager = SettingManager.Instance;
        Assert.IsTrue(settingManager.Mode == SettingMode.Test);
        ISetting setting = settingManager.GetSetting();
        Assert.AreEqual(TestSetting.Get(), setting);
    } 
	
}
