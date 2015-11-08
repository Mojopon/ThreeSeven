using UnityEngine;
using System.Collections.Generic;

public interface IGridSimulator
{
    void SimulateFromOriginalGrid();
    ISimulatedGroup SimulatedGroup { get; }
    bool DropBlocks();
    List<ISimulatedBlock> DeleteBlocks();
    ISimulatedBlock[,] SimulatedGrid { get; }
}
