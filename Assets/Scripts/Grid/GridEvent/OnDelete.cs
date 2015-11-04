using UnityEngine;
using System.Collections.Generic;

public delegate void OnDeleteEventHandler(IGrid grid, List<IBlock> blocksToDelete, int chains);
public interface IOnDeleteSubject
{
    event OnDeleteEventHandler OnDeleteEvent;
    void AddOnDeleteEventListener(IOnDeleteEventListener listener);
    void RemoveOnDeleteEventListener(IOnDeleteEventListener listener);
}

public interface IOnDeleteEventListener
{
    void OnDeleteEvent(IGrid grid, List<IBlock> blocksToDelete, int chains);
}
