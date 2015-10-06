using UnityEngine;
using System.Collections;

public class SettingManager {

    private static SettingManager _instance;

    public static SettingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SettingManager();
            }
            return _instance;
        }
    }

    public SettingMode Mode = SettingMode.Production;

    private SettingManager() { }

    public ISetting GetSetting()
    {
        switch (Mode)
        {
            case SettingMode.Test:
                return TestSetting.Get();
            case SettingMode.Production:
                return GameSetting.Get();
        }

        return null;
    }
}
