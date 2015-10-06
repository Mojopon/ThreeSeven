using UnityEngine;
using System.Collections;

public abstract class GridCommand : IGridCommand {

    protected IGrid _grid;

    public GridCommand(IGrid grid)
    {
        _grid = grid;
    }

    public abstract bool Execute();
}
