using UnityEngine;
using System.Collections;

public class RemoveDeletedBlocksCommand : GridCommand 
{
    public RemoveDeletedBlocksCommand(IGrid grid) : base(grid) { }

    public override bool Execute()
    {
        for (int y = 0; y < _grid.Height; y++)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                if (_grid[x, y] != null && _grid[x, y].IsToDelete)
                {
                    _grid[x, y] = null;
                }
            }
        }

        return true;
    }
}
