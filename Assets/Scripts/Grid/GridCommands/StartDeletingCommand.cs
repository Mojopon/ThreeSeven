﻿using UnityEngine;
using System.Collections.Generic;

public class StartDeletingCommand : GridCommand 
{
    IScoreManager _scoreManager;
    IGameLevelManager _gameLevelManager;
    IFloatingTextRenderer _floatingTextRenderer;

    public StartDeletingCommand(IGrid grid, IScoreManager scoreManager, IGameLevelManager gameLevelManager, IFloatingTextRenderer floatingTextRenderer) : base(grid)
    {
        _scoreManager = scoreManager;
        _gameLevelManager = gameLevelManager;
        _floatingTextRenderer = floatingTextRenderer;
    }

    public override bool Execute()
    {
        var toDelete = BlockComparer.Compare(_grid.GridRaw);

        if (toDelete.Count == 0)
        {
            return false;
        }

        _grid.IncrementChains();
        DoScoreUpdate(_scoreManager, toDelete);
        DoLevelUpdate(_gameLevelManager, toDelete);
        PopupChainMessage(_floatingTextRenderer, toDelete);

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
