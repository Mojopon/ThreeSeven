using UnityEngine;
using System.Collections;

public class FixGroupCommand : GridCommand 
{
    IGroup _currentGroup;
    public FixGroupCommand(IGrid grid, IGroup currentGroup) : base(grid) 
    {
        _currentGroup = currentGroup;
    }

    public override bool Execute()
    {
        foreach (IBlock block in _currentGroup.Children)
        {
            Coord location = block.Location;
            if (!_grid.IsAvailable(location.X, location.Y))
            {
                Debug.Log("Something weird is happenned!");
                return false;
            }

            _grid[location.X, location.Y] = block;
        }

        _currentGroup.Fix();
        return true;
    }
}
