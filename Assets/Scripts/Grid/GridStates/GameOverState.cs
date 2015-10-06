using UnityEngine;
using System.Collections;

public class GameOverState : IGridState {

    public GridStates StateEnum { get { return GridStates.GameOver; } }

    public void OnUpdate()
    {
    }
}
