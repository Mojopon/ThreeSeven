using UnityEngine;
using System.Collections.Generic;

public interface IGridSimulator
{
    void SimulateFromOriginalGrid();
    IGroup SimulatedGroup { get; }
    bool DropBlocks();
    List<ISimulatedBlock> DeleteBlocks();
    ISimulatedBlock[,] SimulatedGrid { get; }
}
