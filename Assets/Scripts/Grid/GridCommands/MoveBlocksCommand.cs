using UnityEngine;
using System.Collections;

public class MoveBlocksCommand : GridCommand 
{
    public MoveBlocksCommand(IGrid grid) : base(grid) { }

    public override bool Execute()
    {
        bool blockMoved = false;
        for (int y = 0; y < _grid.Height; y++)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                if (_grid[x, y] != null)
                {
                    _grid[x, y].OnUpdate();
                    blockMoved = _grid[x, y].IsOnMove == true ? true : blockMoved;
                }
            }
        }

        return blockMoved;
    }
}
