using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;

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

    private Game() { }

    public Game(ISetting setting)
    {
        _setting = setting;
        currentControl = NullControl.Instance;

        if (_setting.IsPlayer)
        {
            InputManager.OnJumpKeyPressed += new InputManager.JumpKeyEvent(OnJumpKeyInput);
            InputManager.OnArrowKeyPressed += new InputManager.ArrowKeyEvent(OnArrowKeyInput);
        }
    }

    public void StartThreeSeven()
    {
        CreateBackGround();
        CreateGrid();
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

    #region IControllable Method Group

    public void OnArrowKeyInput(Direction direction)
    {
        currentControl.OnArrowKeyInput(direction);
    }

    public void OnJumpKeyInput()
    {
        currentControl.OnJumpKeyInput();
    }

    #endregion

    #region IUpdatable Method Group

    public void OnUpdate()
    {
        _grid.OnUpdate();
    }

    #endregion

    #region IGameTextManager Method Group

    public IGameText GetGameText(GameTextType type)
    {
        return _setting.GetGameText(type);
    }

    #endregion
}
