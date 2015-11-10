using UnityEngine;
using System.Collections.Generic;

public interface IGridSimulator
{
    void CreateSimulatedGridOriginal();
    ISimulatedGroup SimulatedGroup { get; }
    ISimulatedBlock[,] SimulatedGrid { get; }

    void CopyOriginalToSimulatedGrid();
    bool SetGroupLocation(Coord location);
    bool RotateGroup();
    bool DropBlocks();
    List<ISimulatedBlock> DeleteBlocks();
    int GetScoreFromSimulation();
}
