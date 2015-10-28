using UnityEngine;
using System.Collections.Generic;

public interface IChainMessagePopup : IOnDeleteEventListener
{
    void PopupChainMessage(List<IBlock> blocksToDelete, int chains);
}
