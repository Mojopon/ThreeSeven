using UnityEngine;
using System.Collections;

public delegate void OnGameOverEventHandler(IGrid grid);

public interface IOnGameOver
{
    event OnGameOverEventHandler OnGameOverEvent;
}
