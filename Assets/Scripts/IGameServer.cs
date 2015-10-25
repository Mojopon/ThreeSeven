using UnityEngine;
using System.Collections;

public interface IGameServer
{
    void Register(IGrid grid);
    void StartNewGame();
    void FinishGame();
}
