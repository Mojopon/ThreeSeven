using UnityEngine;
using System.Collections.Generic;

public class GroupFactory : IGroupFactory {

    private IBlockFactory _blockFactory;
    private List<IGroupPattern> _groupPatternList;

    public GroupFactory(IBlockFactory blockFactory)
    {
        _blockFactory = blockFactory;
    }

    public GroupFactory(IBlockFactory blockFactory, List<IGroupPattern> groupPatternList) : this(blockFactory)
    {
        _groupPatternList = groupPatternList;
    }

    public IGroup Create(ISetting setting, IBlockPattern blockPattern, IGroupPattern groupPattern)
    {
        IGroup group = new Group(setting);

        for (int i = 0; i < groupPattern.Patterns[0].Length; i++)
        {
            IBlock block = _blockFactory.Create(setting, blockPattern.Types[i], groupPattern.Patterns[0][i]);
            group.AddBlock(block);
        }

        group.SetPattern(groupPattern.Patterns);

        return group;
    }

    public IGroup Create(ISetting setting)
    {
        if (_groupPatternList == null || _groupPatternList.Count == 0)
        {
            Debug.Log("Group Pattern List is not to be set!");
            return null;
        }

        IGroup group = new Group(setting);
        Transform groupHolder = null;
        if (setting.IsProduction)
        {
            //groupHolder = new GameObject("Group").transform;
            //group.Parent = groupHolder.transform;
        }

        IGroupPattern groupPattern = _groupPatternList[Random.Range(0, _groupPatternList.Count)];

        for (int i = 0; i < groupPattern.Patterns[0].Length; i++)
        {
            IBlock block = _blockFactory.Create(groupHolder, setting, BlockTypeHelper.GetRandom(), groupPattern.Patterns[0][i]);
            group.AddBlock(block);
        }

        group.SetPattern(groupPattern.Patterns);

        return group;
    }
}
