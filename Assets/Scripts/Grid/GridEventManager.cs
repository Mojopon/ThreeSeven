using UnityEngine;
using System.Collections;

public class GridEventManager : IGridEventManager
{
    IGrid _grid;
    IGroupFactory _groupFactory;

    ISetting _setting;

    private bool _gameIsOver;
    public bool GameIsOver
    {
        get
        {
            return _gameIsOver;
        }
        private set
        {
            _gameIsOver = value;
        }
    }

    public GridEventManager(ISetting setting, IGrid grid, IGroupFactory groupFactory)
    {
        _grid = grid;
        _groupFactory = groupFactory;

        _setting = setting;
    }

    public bool AddGroupToTheGrid(IBlockPattern blockPattern, IGroupPattern groupPattern)
    {
        IGroup group = _groupFactory.Create(_setting, blockPattern, groupPattern);
        return _grid.AddGroup(group);
    }

    public bool AddGroupToTheGrid()
    {
        IGroup group = _groupFactory.Create(_setting);
        return _grid.AddGroup(group);
    }

    public void StartDeleting()
    {
        _grid.StartDeleting();
    }

    void DoDeleteEffect()
    {
        _grid.ProcessDeleting();
    }

    public void DropBlocks()
    {
        _grid.DropBlocks();
    }

    void MoveBlocks()
    {
        _grid.MoveBlocks();
    }

    public void GameOver()
    {
        _grid.GameOver();
    }

    public void OnUpdate()
    {
        switch (_grid.CurrenteStateName)
        {
            case GridStates.Dropped:
                {
                    StartDeleting();
                    break;
                }
            case GridStates.Deleting:
                {
                    DoDeleteEffect();
                    break;
                }
            case GridStates.OnFix:
            case GridStates.Deleted:
                {
                    DropBlocks();
                    break;
                }
            case GridStates.Dropping:
                {
                    MoveBlocks();
                    break;
                }
            case GridStates.ReadyForNextGroup:
                {
                    if (!AddGroupToTheGrid())
                    {
                        GameOver();
                        GameIsOver = true;
                    }
                    break;
                }
            case GridStates.GameOver:
                {
                    break;
                }
        }
    }
}
