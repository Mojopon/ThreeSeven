using UnityEngine;
using System.Collections;

public class NullGameText : IGameText {

    private static NullGameText instance = new NullGameText();
    public static NullGameText Instance
    {
        get
        {
            return instance;
        }
    }


    public void UpdateText(string score)
    {
    }


    public void Disable()
    {
    }
}
