using UnityEngine;
using System.Collections;

public interface IGridState : IUpdatable
{
    GridStates StateEnum { get; }

}
