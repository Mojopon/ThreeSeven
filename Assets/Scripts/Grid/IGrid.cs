using UnityEngine;
using System.Collections;

public interface IGrid : IControllable, IUpdatable
{
    IBlock this[int x, int y] { get; set; }
    int Width { get; }
    int Height { get; }
    int Chains { get; }
    void IncrementChains();
    void ResetChains();
    IBlock[,] GridRaw { get; set; }
    bool ControllingGroup { get; set; }
    GridStates CurrenteStateName { get; }
    void SetState(GridStates state);
    void SetCurrentGroup(IGroup group);
    void Initialize(ISetting setting, IGroupFactory groupFactory);
    void NewGame();
    bool AddGroup(IGroup group);
    bool CanAddGroup(IGroup group);
    bool IsAvailable(int x, int y);
    void FixGroup();
    bool DropBlocks();
    bool MoveBlocks();
    bool StartDeleting();
    bool ProcessDeleting();
    void RemoveDeletedBlocks();
    void GameOver();
}
