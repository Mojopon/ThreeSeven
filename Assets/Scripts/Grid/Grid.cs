using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid : IGrid {

    public event OnGameOverEventHandler OnGameOverEvent;
    public event OnDeleteEventHandler OnDeleteEvent;
    public event OnDeleteEndEventHandler OnDeleteEndEvent;

    public int Width { get { return _grid.GetLength(0); } }
    public int Height { get { return _grid.GetLength(1); } }

    public IGroup CurrentGroup { get { return _currentGroup; } }

    public int CurrentRotatePatternNumber { get
        {
            if(_currentGroup == null)
            {
                return -1;
            }

            return _currentGroup.CurrentRotatePatternNumber;
        }
    }

    public IBlock[,] GridRaw { get { return _grid; } set { _grid = value; } }

    public int Chains { get; private set; }
    public int CurrentScore { get
        {
            if(_scoreManager == null)
            {
                return -1;
            }

            return _scoreManager.Score;
        }
    }

    public void IncrementChains() { Chains++; }
    public void ResetChains() { Chains = 0; }

    public GridStates CurrenteStateName { get { return State.StateEnum; } }

    private IGridState State;
    private IBlock[,] _grid;

    private List<IBlock> _allBlocks;
    private IGameText _gameTextCenter;

    public Grid() 
    {
        _allBlocks = new List<IBlock>();
        State = new GameOverState();
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


    private IGroup _currentGroup;
    public void SetCurrentGroup(IGroup group)
    {
        _currentGroup = group;
    }

    public void SetState(GridStates state)
    {
        // can only set newgame when its gameover
        if(state == GridStates.NewGame)
        {
            State = new NewGameState();
            return;
        }

        if (CurrenteStateName == GridStates.GameOver) return;

        switch (state)
        {
            case GridStates.ReadyForNextGroup:
                {
                    State = new ReadyForNextGroupState(_setting, this, _groupFactory, OnDeleteEndEvent);
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

    private IGroupFactory _groupFactory;
    private ISetting _setting;
    private ICPUManager _cpuManager;
    private IHighScoreManager _highScoreManager;

    public void Initialize(ISetting setting, IGroupFactory groupFactory)
    {
        _setting = setting;
        _groupFactory = groupFactory;
        SetState(GridStates.GameOver);
        if (_setting.GetGameText(GameTextType.GameMessageCenter) == null)
        {
            _gameTextCenter = NullGameText.Instance;
        }
        else
        {
            _gameTextCenter = _setting.GetGameText(GameTextType.GameMessageCenter);
        }

        _cpuManager = new CPUManager(this);

        // initialize highscore if it is a player game.
        if (_setting.IsPlayer)
        {
            _highScoreManager = new HighScoreManager();
        }

        if(_highScoreManager != null)
        {
            DisplayStartMessageAndHighScore();
        }
    }

    void DisplayStartMessageAndHighScore()
    {
        string centerMessage = "Press Space\nTo Start Game";
        int highScore = _highScoreManager.GetHighScore();
        if (highScore > 0)
        {
            centerMessage = "High Score\n" + highScore + "\n\n\n" + centerMessage;
        }
        _gameTextCenter.UpdateText(centerMessage);
    }

    public void NewGame()
    {
        SetState(GridStates.NewGame);

        _grid = new IBlock[_setting.GridWidth, _setting.GridHeight];
        CreateScoreManager(_setting.GetGameText(GameTextType.ScoreText));
        CreateGameLevelManager(_setting);
        CreateChainMessagePopup(_setting.FloatingTextRenderer);

        ClearBlocks();

        _gameTextCenter.Disable();

        SubscribeInputEvents();

        if (!_setting.IsPlayer)
        {
            _cpuManager.ChangeCPUMode(CPUMode.Easy);
        }

        SetState(GridStates.ReadyForNextGroup);
    }

    void ClearBlocks()
    {
        foreach (IBlock block in _allBlocks)
        {
            if (block.IsToDelete) continue;
            block.DeleteImmediate();
        }
        
        _allBlocks.Clear();
    }

    private IScoreManager _scoreManager = NullScoreManager.Instance;
    void CreateScoreManager(IGameText scoreText)
    {
        if(_scoreManager != null)
        {
            RemoveOnDeleteEventListener(_scoreManager);
        }

        _scoreManager = new ScoreManager();
        if (scoreText != null)
        {
            _scoreManager.AttachScoreText(scoreText);
        }

        AddOnDeleteEventListener(_scoreManager);
    }

    private IGameLevelManager _gameLevelManager;
    void CreateGameLevelManager(ISetting setting)
    {
        if(_gameLevelManager != null)
        {
            RemoveOnDeleteEventListener(_gameLevelManager);
        }

        _gameLevelManager = new GameLevelManager(setting, this);
        AddOnDeleteEventListener(_gameLevelManager);
    }

    private IChainMessagePopup _chainMessagePopup;
    void CreateChainMessagePopup(IFloatingTextRenderer floatingTextRenderer)
    {
        if(_chainMessagePopup != null)
        {
            RemoveOnDeleteEventListener(_chainMessagePopup);
        }

        _chainMessagePopup = new ChainMessagePopup(floatingTextRenderer);
        AddOnDeleteEventListener(_chainMessagePopup);
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
        IGridCommand command = new StartDeletingCommand(this, OnDeleteEvent);
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
        if (CurrenteStateName == GridStates.GameOver) return;

        if (_highScoreManager != null)
        {
            _highScoreManager.SetHighScore(_scoreManager.GetScore());
            DisplayGameOverMessage();
        }

        SetState(GridStates.GameOver);
        UnsubscribeInputEvents();

        if (OnGameOverEvent != null) OnGameOverEvent(this);
    }

    void DisplayGameOverMessage()
    {
        _gameTextCenter.UpdateText("Game Over\n\n\nHighScore\n" + _highScoreManager.GetHighScore() + "\n\n\nPress Space\nTo Play Again");
    }

    private bool inputEventsSubscribed = false;
    private void SubscribeInputEvents()
    {
        if (!_setting.IsPlayer || inputEventsSubscribed) return;

        InputManager.OnArrowKeyPressed += new InputManager.ArrowKeyEvent(OnArrowKeyInput);
        InputManager.OnJumpKeyPressed += new InputManager.JumpKeyEvent(OnJumpKeyInput);

        inputEventsSubscribed = true;
    }

    private void UnsubscribeInputEvents()
    {
        if (!_setting.IsPlayer) return;

        InputManager.OnArrowKeyPressed -= new InputManager.ArrowKeyEvent(OnArrowKeyInput);
        InputManager.OnJumpKeyPressed -= new InputManager.JumpKeyEvent(OnJumpKeyInput);

        inputEventsSubscribed = false;
    }

    private bool canControl = true;
    #region IControllable Method Group

    public bool ControllingGroup { get; set; }
    public void OnArrowKeyInput(Direction direction)
    {
        if (_currentGroup == null || ControllingGroup != true || direction == Direction.None) return;

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
        if (CurrenteStateName == GridStates.Paused) return;

        State.OnUpdate();

        if (_gameLevelManager != null)
        {
            _gameLevelManager.OnUpdate();
        }

        _cpuManager.OnUpdate();
    }

    #endregion

    private bool paused = false;
    private IGridState stateBeforePause;

    #region IPauseEvent Method Group

    public void Pause()
    {
        if (CurrenteStateName == GridStates.GameOver) return;

        if (paused == false)
        {
            UnsubscribeInputEvents();
            stateBeforePause = State;
            SetState(GridStates.Paused);
            _gameTextCenter.UpdateText("Paused");
            paused = true;
        }
        else
        {
            SubscribeInputEvents();
            _gameTextCenter.UpdateText("");
            _gameTextCenter.Disable();
            paused = false;
            State = stateBeforePause;
        }
    }

    #endregion

    private List<IOnDeleteEventListener> onDeleteEventListeners;
    #region IOnDeleteSubject method group

    public void AddOnDeleteEventListener(IOnDeleteEventListener listener)
    {
        if (onDeleteEventListeners == null) onDeleteEventListeners = new List<IOnDeleteEventListener>();

        onDeleteEventListeners.Add(listener);
        OnDeleteEvent += new OnDeleteEventHandler(listener.OnDeleteEvent);
    }

    public void RemoveOnDeleteEventListener(IOnDeleteEventListener listener)
    {
        if (onDeleteEventListeners == null) return;

        onDeleteEventListeners.Remove(listener);
        OnDeleteEvent -= new OnDeleteEventHandler(listener.OnDeleteEvent);
    }

    #endregion
}
