using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameManager control;

    public static event JumpKeyEvent OnJumpKeyPressed;
    public delegate void JumpKeyEvent();

    public static event ArrowKeyEvent OnArrowKeyPressed;
    public delegate bool ArrowKeyEvent(Direction direction);

    public static event PauseKeyEvent OnPauseKeyPressed;
    public delegate void PauseKeyEvent();

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

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (OnPauseKeyPressed != null)
            {
                OnPauseKeyPressed();
            }
        }
    }
}
