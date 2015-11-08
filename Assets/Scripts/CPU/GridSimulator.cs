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

    public void SimulateFromOriginalGrid()
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

        if(_grid.CurrentGroup == null)
        {
            return;
        }
        _simulatedGroup = new SimulatedGroup();
        _simulatedGroup.Simulate(_grid.CurrentGroup);
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

        return blocksToDelete;
    }

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
}
