using UnityEngine;
using System.Collections;

public interface ISimulatedBlock : IBlockModel
{
    void SimulateFromBlock(IBlockModel block);
}
