using UnityEngine;
using System.Collections.Generic;

public class AddGroupCommand : GridCommand {

    ISetting _setting;
    IGroup _group;
    List<IBlock> _allBlocks;

    public AddGroupCommand(IGrid grid, ISetting setting, IGroup group, List<IBlock> allBlocks) : base(grid) 
    {
        _setting = setting;
        _group = group;
        _allBlocks = allBlocks;
    }

    public override bool Execute()
    {
        foreach (IBlock block in _group.Children)
        {
            _allBlocks.Add(block);
        }

        _group.SetLocation(_setting.BlockSpawnPoint);

        if (!_grid.CanAddGroup(_group)) return false;

        _grid.SetCurrentGroup(_group);
        _grid.ControllingGroup = true;
        return true;
    }
}
