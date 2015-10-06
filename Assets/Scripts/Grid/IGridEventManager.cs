using UnityEngine;
using System.Collections;

public interface IGridEventManager : IUpdatable {

    bool GameIsOver { get; }

    bool AddGroupToTheGrid(IBlockPattern blockPattern, IGroupPattern groupPattern);
    bool AddGroupToTheGrid();
    void StartDeleting();
    void DropBlocks();
    void GameOver();
}
