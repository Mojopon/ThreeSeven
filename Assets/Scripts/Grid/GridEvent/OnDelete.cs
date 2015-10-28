using UnityEngine;
using System.Collections.Generic;

public delegate void OnDeleteEventHandler(IGrid grid, List<IBlock> blocksToDelete, int chains);
public interface IOnDelete
{
    event OnDeleteEventHandler OnDeleteEvent;
}
