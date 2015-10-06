using UnityEngine;
using System.Collections;

public class PausedState : IGridState {

    public GridStates StateEnum { get { return GridStates.Paused; } }

    public void OnUpdate()
    {

    }
}
