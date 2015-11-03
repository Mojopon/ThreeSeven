using UnityEngine;
using System.Collections;

public interface IGridSimulator
{
    void Simulate();
    IGroup SimulatedGroup { get; }
    IBlockModel[,] SimulatedGrid { get; }
}
