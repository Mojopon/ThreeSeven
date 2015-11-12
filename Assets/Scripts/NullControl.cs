using UnityEngine;
using System.Collections;

public class NullControl : IControllable {

    private static NullControl instance;

    public static NullControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NullControl();
            }

            return instance;
        }
    }

    public void OnArrowKeyInput(Direction direction)
    {
    }

    public void OnJumpKeyInput()
    {
    }
}
