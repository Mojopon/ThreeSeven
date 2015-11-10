using UnityEngine;
using System.Collections;

public interface ISimulatedBlock : IBlockModel
{
    bool FixedOnGrid { get; set; }
    void SimulateFromBlock(IBlockModel block);
}
