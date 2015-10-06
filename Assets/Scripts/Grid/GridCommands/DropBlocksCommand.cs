using UnityEngine;
using System.Collections;

public class DropBlocksCommand : GridCommand 
{
    public DropBlocksCommand(IGrid grid) : base(grid) { }

    public override bool Execute()
    {
        bool dropped = false;
        _grid.GridRaw = BlockDropper.GetGridAfterDrop(_grid.GridRaw, out dropped);
        for (int y = 0; y < _grid.Height; y++)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                if (_grid[x, y] != null)
                {
                    _grid[x, y].MoveToLocation(new Coord(x, y));
                }
            }
        }

        return dropped;
    }
}
