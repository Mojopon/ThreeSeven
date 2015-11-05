using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SimulatedGroup : ISimulatedGroup
{
    private List<ISimulatedBlock> _blocks;
    public Coord Location { get; private set; }
    private List<Coord[]> _patterns;
    private IRotatePatternManager _rotatePatternManager;

    public SimulatedGroup()
    {
        _blocks = new List<ISimulatedBlock>();
    }

    public ISimulatedBlock[] Children { get { return _blocks.ToArray(); } }

    public Coord[] ChildrenLocation
    {
        get
        {
            Coord[] coords = new Coord[_blocks.Count];
            for (int i = 0; i < _blocks.Count; i++)
            {
                coords[i] = _blocks[i].Location;
            }

            return coords;
        }
    }

    public void Simulate(IGroup target)
    {
        Location = target.Location;
        _patterns = target.GetPattern();
        _rotatePatternManager = new RotatePatternManager(_patterns);

        foreach(IBlock block in target.Children)
        {
            AddBlock(new SimulatedBlock(block));
        }
    }


    public void AddBlock(ISimulatedBlock block)
    {
        _blocks.Add(block);
    }

    public List<Coord[]> GetPattern()
    {
        return _patterns;
    }

    public void Move(Direction direction)
    {
        throw new NotImplementedException();
    }

    public void Rotate(RotateDirection rotateDirection)
    {
        throw new NotImplementedException();
    }

    public void SetLocation(Coord location)
    {
        throw new NotImplementedException();
    }

}
