using UnityEngine;
using System.Collections;

public class GameStartSettingValues : MonoBehaviour {

    public CPUMode difficulty;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
