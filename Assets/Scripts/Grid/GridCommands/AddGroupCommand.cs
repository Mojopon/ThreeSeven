using UnityEngine;
using System.Collections.Generic;

public class AddGroupCommand : GridCommand {

    private ISetting _setting;
    private IGroup _group;
    private List<IBlock> _allBlocks;
    private OnGroupAddEventHandler _onGroupAddEvent;

    public AddGroupCommand(IGrid grid, ISetting setting, IGroup group, List<IBlock> allBlocks, OnGroupAddEventHandler onGroupAddEvent) : base(grid) 
    {
        _setting = setting;
        _group = group;
        _allBlocks = allBlocks;
        _onGroupAddEvent = onGroupAddEvent;
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

        if (_onGroupAddEvent != null) _onGroupAddEvent(_grid, _group);
        return true;
    }
}
