using UnityEngine;
using System.Collections;

public delegate void OnGroupAddEventHandler(IGrid grid, IGroup group);
public interface IOnGroupAdd
{
    event OnGroupAddEventHandler OnGroupAdd;
}
