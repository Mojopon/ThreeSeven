using UnityEngine;
using System.Collections;

public class GridSimulator : IGridSimulator
{
    private IGrid _grid;
    public GridSimulator(IGrid grid)
    {
        _grid = grid;
    }


}
