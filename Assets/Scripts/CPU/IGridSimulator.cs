using UnityEngine;
using System.Collections.Generic;

public interface IGridSimulator
{
    void CreateSimulatedGridOriginal();
    ISimulatedGroup SimulatedGroup { get; }
    ISimulatedBlock[,] SimulatedGrid { get; }

    void CopyOriginalToSimulatedGrid();
    void SetGroupLocation(Coord location);
    void RotateGroup();
    bool AdjustGroupPosition();
    bool DropBlocks();
    List<ISimulatedBlock> DeleteBlocks();
    int GetScoreFromSimulation();
}
