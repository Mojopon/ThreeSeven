using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ChainMessagePopup : IChainMessagePopup
{
    private IFloatingTextRenderer _textRenderer;
    public ChainMessagePopup(IFloatingTextRenderer floatingTextRenderer)
    {
        _textRenderer = floatingTextRenderer;
    }

    public void OnDeleteEvent(IGrid grid, List<IBlock> blocksToDelete, int chains)
    {
        PopupChainMessage(blocksToDelete, chains);
    }

    public void PopupChainMessage(List<IBlock> blocksToDelete, int chains)
    {
        Vector2 position = blocksToDelete[0].WorldPosition;

        string chainMessage = " Chains!";

        if (chains == 1)
            chainMessage = " Chain!";

        _textRenderer.RenderText(position, chains + chainMessage);
    }
}
