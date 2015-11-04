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

    public void Simulate()
    {
        _simulatedGrid = new SimulatedBlock[_setting.GridWidth, _setting.GridHeight];

        for (int y = 0; y < _setting.GridHeight; y++)
        {
            for (int x = 0; x < _setting.GridWidth; x++)
            {
                if(_grid[x, y] != null)
                {
                    _simulatedGrid[x, y] = new SimulatedBlock(_grid[x, y]);
                }
            }
        }
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
        BlockComparer.Compare(SimulatedGrid);



        return null;
    }

    private ISimulatedBlock[,] _simulatedGrid;
    public ISimulatedBlock[,] SimulatedGrid
    {
        get { return _simulatedGrid; }
    }

    private IGroup _simulatedGroup;
    public IGroup SimulatedGroup
    {
        get { return _simulatedGroup; }
    }
}
