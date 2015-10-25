using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;

public class Game : IGame
{
    public ICameraManager CameraManager { get; set; }
    public IBackgroundFactory BackgroundFactory { get; set; }
    public IGridFactory GridFactory { get; set; }
    public IGroupFactory GroupFactory { get; set; }

    private IGrid _grid;

    #region ISetting Group

    public Transform Parent { get { return _setting.Parent; } set { _setting.Parent = value; } }
    public IParticleSpawner ParticleSpawner { get { return _setting.ParticleSpawner; } set { _setting.ParticleSpawner = value; } }
    public IBlockColorRepository BlockColorRepository { get { return _setting.BlockColorRepository; } set { _setting.BlockColorRepository = value; } }
    public IFloatingTextRenderer FloatingTextRenderer { get { return _setting.FloatingTextRenderer; } set { _setting.FloatingTextRenderer = value; } }

    public bool IsPlayer { get { return _setting.IsPlayer; } }
    public float ScalePerBlock { get { return _setting.ScalePerBlock; } }
    public int GridWidth { get { return _setting.GridWidth; } }
    public int GridHeight { get { return _setting.GridHeight; } }
    public Vector3 GridPosition { get { return _setting.GridPosition; } }
    public Coord BlockSpawnPoint { get { return _setting.BlockSpawnPoint; } }
    public float BlockFallSpeed { get { return _setting.BlockFallSpeed; } }
    public float BlockDeleteSpeed { get { return _setting.BlockDeleteSpeed; } }
    public float WaitAfterDelete { get { return _setting.WaitAfterDelete; } }
    public Vector3[] StockPositions { get { return _setting.StockPositions; } }

    public Vector3 GetGridCenterPosition()
    {
        return _setting.GetGridCenterPosition();
    }

    public Vector3 GetGridScale()
    {
        return _setting.GetGridScale();
    }

    #endregion

    private IControllable currentControl;
    private ISetting _setting;
    private bool paused = false;

    private Game() { }

    public Game(IGrid grid, ISetting setting)
    {
        _setting = setting;
        _grid = grid;

        currentControl = _grid;
    }

    public Game(ISetting setting)
    {
        _setting = setting;
        currentControl = NullControl.Instance;
    }

    public void InitializeGrid()
    {
        CreateBackGround();
        CreateGrid();
    }

    public void RegisterTheGridToTheGameServer(IGameServer gameServer)
    {
        gameServer.Register(_grid);
    }

    void CreateBackGround()
    {
        if (BackgroundFactory != null)
        {
            var background = BackgroundFactory.Create(this);
            SetCameraPosition(background.Center);
        }
    }

    void CreateGrid()
    {
        if (GridFactory != null)
        {
            var grid = GridFactory.Create();
            _grid = grid;

            currentControl = _grid;
        }
    }

    void SetCameraPosition(Vector3 cameraPosition)
    {
        if (CameraManager != null)
        {
            CameraManager.SetCameraPosition(cameraPosition);
        }
    }

    private bool jumpKeyEventSubscribed = false;
    void SubscribeJumpKeyEvent()
    {
        if (jumpKeyEventSubscribed) return;

        InputManager.OnJumpKeyPressed += new InputManager.JumpKeyEvent(OnJumpKeyInput);
        jumpKeyEventSubscribed = true;
    }

    void UnsubscribeJumpKeyEvent()
    {
        InputManager.OnJumpKeyPressed -= new InputManager.JumpKeyEvent(OnJumpKeyInput);
        jumpKeyEventSubscribed = false;
    }

    #region IControllable Method Group

    public void OnArrowKeyInput(Direction direction)
    {
        currentControl.OnArrowKeyInput(direction);
    }

    public void OnJumpKeyInput()
    {
        _grid.NewGame();
        UnsubscribeJumpKeyEvent();
    }

    #endregion

    #region IUpdatable Method Group

    public void OnUpdate()
    {
        if (paused) return;

        _grid.OnUpdate();
        if (_grid.CurrenteStateName == GridStates.GameOver)
        {
            SubscribeJumpKeyEvent();
        }
    }

    #endregion

    #region IGameTextManager Method Group

    public IGameText GetGameText(GameTextType type)
    {
        return _setting.GetGameText(type);
    }

    #endregion

    #region IPauseEvent Method Group

    public void Pause()
    {
        if (_grid.CurrenteStateName == GridStates.GameOver) return;

        if (!paused)
        {
            paused = true;
            if (_setting.Parent != null)
            {
                _setting.Parent.gameObject.SetActive(false);
            }
        }
        else
        {
            paused = false;
            if (_setting.Parent != null)
            {
                _setting.Parent.gameObject.SetActive(true);
            }
        }

        _grid.Pause();
    }

    #endregion
}
