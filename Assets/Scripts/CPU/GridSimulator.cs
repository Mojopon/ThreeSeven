using UnityEngine;
using System.Collections;
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

        return dropped;
    }

    private IBlockModel[,] _simulatedGrid;
    public IBlockModel[,] SimulatedGrid
    {
        get { return _simulatedGrid; }
    }

    private IGroup _simulatedGroup;
    public IGroup SimulatedGroup
    {
        get { return _simulatedGroup; }
    }
}
