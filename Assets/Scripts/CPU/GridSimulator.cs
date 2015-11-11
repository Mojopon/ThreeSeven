using UnityEngine;
using System.Collections.Generic;
using System;

public class GridSimulator : IGridSimulator
{
    private IGrid _grid;
    private ISetting _setting;
    public GridSimulator(IGrid grid, ISetting setting)
    {
        _grid = grid;
        _setting = setting;
        _simulatedGroup = new SimulatedGroup();
    }

    private ISimulatedBlock[,] _simulatedGridOriginal;

    private ISimulatedBlock[,] _simulatedGrid;
    public ISimulatedBlock[,] SimulatedGrid
    {
        get { return _simulatedGrid; }
    }

    private ISimulatedGroup _simulatedGroup;
    public ISimulatedGroup SimulatedGroup
    {
        get { return _simulatedGroup; }
    }

    public void CreateSimulatedGridOriginal()
    {
        _simulatedGridOriginal = new SimulatedBlock[_setting.GridWidth, _setting.GridHeight];

        for (int y = 0; y < _setting.GridHeight; y++)
        {
            for (int x = 0; x < _setting.GridWidth; x++)
            {
                if (_grid[x, y] != null)
                {
                    _simulatedGridOriginal[x, y] = new SimulatedBlock(_grid[x, y]);
                }
            }
        }

        _simulatedGrid = new SimulatedBlock[_setting.GridWidth, _setting.GridHeight];

        CopyOriginalToSimulatedGrid();

        if (_grid.CurrentGroup == null)
        {
            return;
        }
        _simulatedGroup.Simulate(_grid.CurrentGroup);
    }

    public void CopyOriginalToSimulatedGrid()
    {
        for (int y = 0; y < _setting.GridHeight; y++)
        {
            for (int x = 0; x < _setting.GridWidth; x++)
            {
                _simulatedGrid[x, y] = _simulatedGridOriginal[x, y];
                if(_simulatedGrid[x, y] != null)
                {
                    _simulatedGrid[x, y].SetLocation(new Coord(x, y));
                }
            }
        }
    }

    public void SetGroupLocation(Coord location)
    {
        
        var locationBefore = SimulatedGroup.Location;
        SimulatedGroup.SetLocation(location);
    }

    public void RotateGroup()
    {
        SimulatedGroup.Rotate(RotateDirection.Clockwise);
    }

    public bool DropBlocks()
    {
        bool dropped;
        _simulatedGrid = BlockDropper.GetGridAfterDrop(_simulatedGrid, out dropped);

        
        for(int y = 0; y < _setting.GridHeight; y++)
        {
            for(int x = 0; x < _setting.GridWidth; x++)
            {
                if (_simulatedGrid[x, y] != null)
                {
                    _simulatedGrid[x, y].SetLocation(new Coord(x, y));
                }
            }
        }
        

        return dropped;
    }

    public bool AdjustGroupPosition()
    {
        var locationBeforeAdjust = SimulatedGroup.Location;
        while(SimulatedGroup.Location.Y >= 0)
        {
            if(CanFixGroup())
            {
                return true;
            }

            SimulatedGroup.SetLocation(SimulatedGroup.Location + Direction.Down.ToCoord());
        }

        SimulatedGroup.SetLocation(locationBeforeAdjust);
        return false;
    }

    public List<ISimulatedBlock> DeleteBlocks()
    {
        var blocksToDelete = BlockComparer.Compare(SimulatedGrid);
        
        foreach (IBlockModel block in blocksToDelete)
        {
            _simulatedGrid[block.Location.X, block.Location.Y] = null;
        }

        return blocksToDelete;
    }

    public int GetScoreFromSimulation()
    {
        if (!AdjustGroupPosition()) return -1;

        foreach(ISimulatedBlock block in SimulatedGroup.Children)
        {
            if(SimulatedGrid[block.Location.X, block.Location.Y] != null)
            {
                return -2;
            }

            block.FixedOnGrid = true;
            SimulatedGrid[block.Location.X, block.Location.Y] = block;
        }
        

        bool simulationDone = false;
        int totalScore = 0;
        int chains = 1;
        while(!simulationDone)
        {
            DropBlocks();

            var deletedBlocks = DeleteBlocks();

            if (deletedBlocks.Count != 0)
            {
                totalScore += ScoreCalculator.Calculate(deletedBlocks, chains++);
            }
            else
            {
                simulationDone = true;
            }
        }

        CopyOriginalToSimulatedGrid();

        foreach (ISimulatedBlock block in SimulatedGroup.Children)
        {
            block.FixedOnGrid = false;
        }
        SimulatedGroup.SetLocation(SimulatedGroup.Location);

        return totalScore;
    }

    bool IsOutOfRange(int x, int y)
    {
        if(x < 0 || y < 0 || x >= _setting.GridWidth || y >= _setting.GridHeight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsOutOfRange(Coord coord)
    {
        return IsOutOfRange(coord.X, coord.Y);
    }

    bool IsAvailable(int x, int y)
    {
        if(IsOutOfRange(x, y) || SimulatedGrid[x, y] != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool IsAvailable(Coord coord)
    {
        return IsAvailable(coord.X, coord.Y);
    }

    bool CanFixGroup()
    {
        foreach(ISimulatedBlock block in SimulatedGroup.Children)
        {
            if(!IsAvailable(block.Location))
            {
                return false;
            }
        }

        return true;
    }
}
