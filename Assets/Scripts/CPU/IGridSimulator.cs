using UnityEngine;
using System.Collections.Generic;

public interface IGridSimulator
{
    void SimulateFromOriginalGrid();
    ISimulatedGroup SimulatedGroup { get; }
    ISimulatedBlock[,] SimulatedGrid { get; }

    bool DropBlocks();
    List<ISimulatedBlock> DeleteBlocks();
    int GetScoreFromSimulation();
}
