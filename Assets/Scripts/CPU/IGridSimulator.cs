using UnityEngine;
using System.Collections;

public interface IGridSimulator
{
    void Simulate();
    IGroup SimulatedGroup { get; }
    bool DropBlocks();
    IBlockModel[,] SimulatedGrid { get; }
}
