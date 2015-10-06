using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : IGrid {

    IBlock[,] _grid;

    List<IBlock> _allBlocks;
    private IGameText _gameTextCenter;

    public Grid() 
    {
        _allBlocks = new List<IBlock>();
    }

    public IBlock this[int x, int y]
    {
        get
        {
            if (!IsOutOfRange(x, y))
            {
                return _grid[x, y];
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (!IsOutOfRange(x, y))
            {
                _grid[x, y] = value;
            }
        }
    }

    public int Width { get { return _grid.GetLength(0); } }
    public int Height { get { return _grid.GetLength(1); } }

    public IBlock[,] GridRaw { get { return _grid; } set { _grid = value; } }

    public int Chains { get; private set; }
    public void IncrementChains() { Chains++; }
    public void ResetChains() { Chains = 0; }

    private IGridState State;
    public GridStates CurrenteStateName { get { return State.StateEnum; } }

    private IGroup _currentGroup;
    public void SetCurrentGroup(IGroup group)
    {
        _currentGroup = group;
    }

    public void SetState(GridStates state)
    {
        switch (state)
        {
            case GridStates.ReadyForNextGroup:
                {
                    State = new ReadyForNextGroupState(_setting, this, _groupFactory);
                }
                break;
            case GridStates.Deleted:
                {
                    State = new DeletedState(this);
                }
                break;
            case GridStates.Deleting:
                {
                    State = new DeletingState(this);
                }
                break;
            case GridStates.Dropped:
                {
                    State = new DroppedState(this);
                }
                break;
            case GridStates.Dropping:
                {
                    State = new DroppingState(this);
                }
                break;
            case GridStates.GameOver:
                {
                    State = new GameOverState();
                }
                break;
            case GridStates.OnControlGroup:
                {
                    State = new OnControlGroupState(this);
                }
                break;
            case GridStates.OnFix:
                {
                    State = new OnFixState(this);
                }
                break;
            case GridStates.Paused:
                {
                    State = new PausedState();
                }
                break;
        }
    }

    IGroupFactory _groupFactory;
    ISetting _setting;

    public void Initialize(ISetting setting, IGroupFactory groupFactory)
    {
        _setting = setting;
        _groupFactory = groupFactory;
        SetState(GridStates.GameOver);
        _gameTextCenter = _setting.GetGameText(GameTextType.GameMessageCenter);
        _gameTextCenter.UpdateText("Press Space\nTo Start Game");
    }

    public void NewGame()
    {
        _grid = new IBlock[_setting.GridWidth, _setting.GridHeight];
        CreateScoreManager(_setting.GetGameText(GameTextType.ScoreText));
        CreateGameLevelManager(_setting);

        foreach (IBlock block in _allBlocks)
        {
            if (block.IsToDelete) continue;
            block.DeleteImmediate();
        }
        _allBlocks.Clear();

        _gameTextCenter.Disable();

        SetState(GridStates.ReadyForNextGroup);
    }

    private IScoreManager _scoreManager = NullScoreManager.Instance;
    void CreateScoreManager(IGameText scoreText)
    {
        _scoreManager = new ScoreManager();
        if (scoreText != null)
        {
            _scoreManager.AttachScoreText(scoreText);
        }
    }

    private IGameLevelManager _gameLevelManager;
    void CreateGameLevelManager(ISetting setting)
    {
        _gameLevelManager = new GameLevelManager(setting, this);
    }

    public bool AddGroup(IGroup group)
    {
        IGridCommand command = new AddGroupCommand(this, _setting, group, _allBlocks);
        return command.Execute();
    }

    public bool CanAddGroup(IGroup group)
    {
        foreach(Coord coord in group.ChildrenLocation) 
        {
            if (!IsAvailable(coord.X, coord.Y)) return false;
        }

        return true;
    }

    public bool IsAvailable(int x, int y)
    {
        if (IsOutOfRange(x, y) || IsOnBlock(x, y)) return false;

        return true;
    }

    bool IsOnBlock(int x, int y)
    {
        if (_grid[x, y] != null) return true;

        return false;
    }

    bool IsOutOfRange(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height) return true;

        return false;
    }

    void RotateCurrentGroup()
    {
        _currentGroup.Rotate(RotateDirection.Clockwise);

        foreach (Coord coord in _currentGroup.ChildrenLocation)
        {
            if (!IsAvailable(coord.X, coord.Y))
            {
                _currentGroup.Rotate(RotateDirection.Counterclockwise);
                return;
            }
        }
    }

    public void FixGroup()
    {
        IGridCommand command = new FixGroupCommand(this, _currentGroup);
        command.Execute();
    }

    private bool hasMovingBlock = false;
    public bool DropBlocks()
    {
        IGridCommand command = new DropBlocksCommand(this);
        return command.Execute();
    }

    public bool MoveBlocks()
    {
        IGridCommand command = new MoveBlocksCommand(this);
        return command.Execute();
    }

    public bool StartDeleting()
    {
        IGridCommand command = new StartDeletingCommand(this, _scoreManager, _gameLevelManager, _setting.FloatingTextRenderer);
        return command.Execute();
    }

    public bool ProcessDeleting()
    {
        IGridCommand command = new ProcessDeletingCommand(this);
        return command.Execute();
    }

    public void RemoveDeletedBlocks()
    {
        IGridCommand command = new RemoveDeletedBlocksCommand(this);
        command.Execute();
    }

    public void GameOver()
    {
        _gameTextCenter.UpdateText("Game Over\n\nPress Space\nTo Play Again");
        SetState(GridStates.GameOver);
    }

    private bool canControl = true;
    #region IControllable Method Group

    public bool ControllingGroup { get; set; }
    public void OnArrowKeyInput(Direction direction)
    {
        if (_currentGroup == null || ControllingGroup != true) return;

        if (direction == Direction.Up)
        {
            RotateCurrentGroup();
            return;
        }

        bool bumpedOnTheGround = false;

        _currentGroup.Move(direction);
        foreach (Coord coord in _currentGroup.ChildrenLocation)
        {
            if (!IsAvailable(coord.X, coord.Y))
            {
                _currentGroup.Move(direction.GetOpposite());
                if (direction == Direction.Down)
                {
                    bumpedOnTheGround = true;
                }
                break;
            }
        }

        if (bumpedOnTheGround)
        {
            ControllingGroup = false;
            _currentGroup.PlaySound(SoundName.BumpOnTheGround);
        }
    }

    public void OnJumpKeyInput()
    {
        if (CurrenteStateName == GridStates.GameOver)
        {
            NewGame();
        }

        if (_currentGroup == null || ControllingGroup != true) return;

        Direction direction = Direction.Down;
        bool bumpedOnTheFloor = false;
        while (true)
        {
            _currentGroup.Move(direction);
            foreach (Coord coord in _currentGroup.ChildrenLocation)
            {
                if (!IsAvailable(coord.X, coord.Y))
                {
                    _currentGroup.Move(direction.GetOpposite());
                    bumpedOnTheFloor = true;
                    _currentGroup.PlaySound(SoundName.BumpOnTheGround);
                    break;
                }
            }

            if (bumpedOnTheFloor)
            {
                ControllingGroup = false;
                break;
            }
        }
    }

    #endregion;

    #region IUpdatable Group

    public void OnUpdate()
    {
        State.OnUpdate();

        if (_gameLevelManager != null)
        {
            _gameLevelManager.OnUpdate();
        }
    }

    #endregion
}
