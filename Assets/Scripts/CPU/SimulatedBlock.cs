using UnityEngine;
using System.Collections;
using System;

public class SimulatedBlock : ISimulatedBlock
{
    public BlockType BlockType { get; private set; }
    public int Number { get; private set; }

    public Coord Location { get; private set; }
    public Coord LocationInTheGroup { get; set; }

    public bool FixedOnGrid { get; set; }

    public SimulatedBlock()
    {
        BlockType = BlockType.None;
        Number = -1;
    }

    public SimulatedBlock(IBlockModel block)
    {
        SimulateFromBlock(block);
    }

    public void SimulateFromBlock(IBlockModel block)
    {
        Number = block.Number;
        BlockType = block.BlockType;

        Location = block.Location;
        LocationInTheGroup = block.LocationInTheGroup;
    }

    public void SetLocation(Coord location)
    {
        if(FixedOnGrid)
        {
            Location = location;
            return;
        }

        Location = location + LocationInTheGroup;
    }

    public void Move(Coord velocity)
    {
        Location += velocity + Location;
    }
}
