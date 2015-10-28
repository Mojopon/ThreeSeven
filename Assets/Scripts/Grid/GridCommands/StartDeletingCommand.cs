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
        var toDelete = BlockComparer.Compare(_grid.GridRaw);

        if (toDelete.Count == 0)
        {
            return false;
        }

        _grid.IncrementChains();

        if (_onDeleteEvent != null)
        {
            _onDeleteEvent(_grid, toDelete, _grid.Chains);
        }

        foreach (IBlock block in toDelete)
        {
            block.StartDeleting();
        }

        return true;
    }
}
