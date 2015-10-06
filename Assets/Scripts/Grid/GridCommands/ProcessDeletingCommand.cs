using UnityEngine;
using System.Collections;

public class ProcessDeletingCommand : GridCommand 
{
    public ProcessDeletingCommand(IGrid grid) : base(grid) { }

    public override bool Execute()
    {
        bool stillDeleting = false;
        for (int y = 0; y < _grid.Height; y++)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                if (_grid[x, y] != null && _grid[x, y].IsToDelete)
                {
                    _grid[x, y].OnUpdate();
                    stillDeleting = _grid[x, y].IsDeleting == true ? true : stillDeleting;
                }
            }
        }

        if (!stillDeleting)
        {
            _grid.RemoveDeletedBlocks();
            return false;
        }

        return true;
    }
}
