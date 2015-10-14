using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameEntity control;

    public static event JumpKeyEvent OnJumpKeyPressed;
    public delegate void JumpKeyEvent();

    public static event ArrowKeyEvent OnArrowKeyPressed;
    public delegate void ArrowKeyEvent(Direction direction);

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (OnJumpKeyPressed != null)
            {
                OnJumpKeyPressed();
            }
            return;
        }

        Direction inputtedDirection = Direction.None;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputtedDirection = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputtedDirection = Direction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputtedDirection = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputtedDirection = Direction.Right;
        }

        if (inputtedDirection != Direction.None)
        {
            if (OnArrowKeyPressed != null)
            {
                OnArrowKeyPressed(inputtedDirection);
            }
        }

        /*if (Input.GetButtonDown("Jump"))
        {
            control.OnJumpKeyInput();
            return;
        }*/
        /*
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
        }*/
    }
}
