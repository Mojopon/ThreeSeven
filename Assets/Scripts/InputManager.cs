using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameEntity control;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            control.OnJumpKeyInput();
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            control.OnArrowKeyInput(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            control.OnArrowKeyInput(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            control.OnArrowKeyInput(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            control.OnArrowKeyInput(Direction.Right);
        }
    }
}
