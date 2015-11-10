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
        _simulatedGroup = new SimulatedGroup();
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

    public bool SetGroupLocation(Coord location)
    {
        /*
        var locationBefore = SimulatedGroup.Location;
        SimulatedGroup.SetLocation(location);
        foreach (ISimulatedBlock block in SimulatedGroup.Children)
        {
            if (SimulatedGrid[block.Location.X, block.Location.Y] != null)
            {
                SimulatedGroup.SetLocation(locationBefore);
                return false;
            }
        }

    */
        return true;
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
        var tickCount = System.Environment.TickCount;

        foreach(ISimulatedBlock block in SimulatedGroup.Children)
        {
            if(SimulatedGrid[block.Location.X, block.Location.Y] != null)
            {
                Debug.Log("Theres block in the location where group is. aborting simulate");
                return -1;
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
}
