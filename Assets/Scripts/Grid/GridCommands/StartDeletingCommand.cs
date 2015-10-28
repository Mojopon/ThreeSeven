using UnityEngine;
using System.Collections.Generic;

public class StartDeletingCommand : GridCommand 
{
    private IFloatingTextRenderer _floatingTextRenderer;

    private OnDeleteEventHandler _onDeleteEvent;

    public StartDeletingCommand(IGrid grid, IFloatingTextRenderer floatingTextRenderer, OnDeleteEventHandler onDeleteEvent) : base(grid)
    {
        _floatingTextRenderer = floatingTextRenderer;
        _onDeleteEvent = onDeleteEvent;
    }

    public override bool Execute()
    {
        var toDelete = BlockComparer.Compare(_grid.GridRaw);

        if (toDelete.Count == 0)
        {
            return false;
        }

        _grid.IncrementChains();
        PopupChainMessage(_floatingTextRenderer, toDelete);

        if (_onDeleteEvent != null)
        {
            _onDeleteEvent(_grid, toDelete, _grid.Chains);
        }

        foreach (IBlock block in toDelete)
        {
            block.StartDeleting();
        }

        return true;
    }

    void DoScoreUpdate(IScoreManager scoreManager, List<IBlock> toDelete)
    {
        scoreManager.OnDeleteBlocks(toDelete, _grid.Chains);
    }

    void DoLevelUpdate(IGameLevelManager gameLevelManager, List<IBlock> toDelete)
    {
        gameLevelManager.OnBlockDelete(toDelete);
    }

    void PopupChainMessage(IFloatingTextRenderer floatingTextRenderer, List<IBlock> toDelete)
    {

        Vector2 position = toDelete[0].WorldPosition;

        string chainMessage = " Chains!";

        if (_grid.Chains == 1)
            chainMessage = " Chain!";

        _floatingTextRenderer.RenderText(position, _grid.Chains + chainMessage);
    }
}
