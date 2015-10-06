using UnityEngine;
using System.Collections;

public interface IControllable 
{
    void OnArrowKeyInput(Direction direction);
    void OnJumpKeyInput();
}
