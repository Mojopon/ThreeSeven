using UnityEngine;
using System.Collections;

public class NewGameState : IGridState
{
    public GridStates StateEnum { get { return GridStates.NewGame; } }

    public void OnUpdate()
    {
    }
}
