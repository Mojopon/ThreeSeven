using UnityEngine;
using System.Collections;

public interface IControllable 
{
    bool OnArrowKeyInput(Direction direction);
    void OnJumpKeyInput();
}
