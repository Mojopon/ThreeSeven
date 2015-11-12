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
    public int CurrentRotatePatternNumber
    {
        get
        {
            if (_rotatePatternManager == null)
            {
                return -1;
            }

            return _rotatePatternManager.CurrentRotatePatternNumber;
        }
    }
    public int RotationPatternNumber
    {
        get
        {
            if (_rotatePatternManager == null)
            {
                return -1;
            }

            return _rotatePatternManager.RotationPatternNumber;
        }
    }

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
        _rotatePatternManager = new RotatePatternManager(_patterns, target.CurrentRotatePatternNumber);

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
        Coord nextLocation = Location + direction.ToCoord();
        SetLocation(nextLocation);
    }

    public void Rotate(RotateDirection rotateDirection)
    {
        var currentPattern = _rotatePatternManager.GetRotatedPattern(rotateDirection);

        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].LocationInTheGroup = currentPattern[i];
        }

        SetLocation(Location);
    }

    public void SetLocation(Coord location)
    {
        Location = location;

        foreach(ISimulatedBlock block in _blocks)
        {
            block.SetLocation(Location);
        }
    }

}
