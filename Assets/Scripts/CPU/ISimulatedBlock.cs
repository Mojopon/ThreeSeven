using UnityEngine;
using System.Collections;

public interface ISimulatedBlock : IBlockModel
{
    void SimulateFrom(IBlock block);
}
