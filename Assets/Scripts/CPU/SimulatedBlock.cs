using UnityEngine;
using System.Collections;
using System;

public class SimulatedBlock : ISimulatedBlock
{
    public BlockType BlockType { get; private set; }
    public int Number { get; private set; }

    public SimulatedBlock()
    {
        BlockType = BlockType.None;
        Number = -1;
    }

    public SimulatedBlock(IBlock block)
    {
        SimulateFrom(block);
    }

    public void SimulateFrom(IBlock block)
    {
        Number = block.Number;
        BlockType = block.BlockType;
    }
}
