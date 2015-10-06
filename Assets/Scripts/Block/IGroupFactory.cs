using UnityEngine;
using System.Collections;

public interface IGroupFactory
{
    IGroup Create(ISetting setting, IBlockPattern blockPattern, IGroupPattern groupPattern);
    IGroup Create(ISetting setting);
}
