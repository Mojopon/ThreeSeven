using UnityEngine;
using System.Collections;

public delegate void OnGameOver(IGrid grid);

public interface IOnGameOver
{
    event OnGameOver OnGameOverEvent;
}
