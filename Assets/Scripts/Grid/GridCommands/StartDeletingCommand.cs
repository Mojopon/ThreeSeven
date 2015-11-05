using UnityEngine;
using System.Collections.Generic;

public class StartDeletingCommand : GridCommand 
{
    private OnDeleteEventHandler _onDeleteEvent;

    public StartDeletingCommand(IGrid grid, OnDeleteEventHandler onDeleteEvent) : base(grid)
    {
        _onDeleteEvent = onDeleteEvent;
    }

    public override bool Execute()
    {
        var blocksToDelete = BlockComparer.Compare(_grid.GridRaw);

        if (blocksToDelete.Count == 0)
        {
            return false;
        }

        _grid.IncrementChains();

        foreach (IBlock block in blocksToDelete)
        {
            block.StartDeleting();
        }

        if (_onDeleteEvent != null)
        {
            _onDeleteEvent(_grid, blocksToDelete, _grid.Chains);
        }

        return true;
    }
}
