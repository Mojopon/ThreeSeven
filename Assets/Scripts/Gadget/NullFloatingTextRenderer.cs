using UnityEngine;
using System.Collections;

public class NullFloatingTextRenderer : IFloatingTextRenderer {

    private static NullFloatingTextRenderer instance;
    public static NullFloatingTextRenderer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NullFloatingTextRenderer();
            }

            return instance;
        }
    }

    private NullFloatingTextRenderer() { }

    public void RenderText(Vector2 position, string message)
    {

    }
}
