using UnityEngine;
using System.Collections;
using System;

public class GameSetting : MonoBehaviour, ISetting {

    private bool isProduction = true;

    [SerializeField]
    private bool isPlayer = true;
    [SerializeField]
    private CPUMode cpuDifficulty = CPUMode.Normal;
    [SerializeField]
    private float scalePerBlock = 1f;
    [SerializeField]
    private int gridWidth = 7;
    [SerializeField]
    private int gridHeight = 14;
    [SerializeField]
    private Vector3 playerGridPosition = Vector3.zero;
    [SerializeField]
    private Coord blockPopPoint = new Coord(1, 1);
    [SerializeField]
    private float blockFallSpeed = 10f;
    [SerializeField]
    private float blockDeleteSpeed = 10f;
    [SerializeField]
    private float waitAfterDelete = 0.5f;
    [SerializeField]
    private StockDisplayConfig[] stockPositions;
    [SerializeField]
    private GameTextComponent[] gameTextComponents;
    private IParticleSpawner particleSpawner = NullParticleSpawner.Instance;
    private IBlockColorRepository blockColorRepository = NullBlockColorRepository.Instance;
    private IFloatingTextRenderer floatingTextRenderer = NullFloatingTextRenderer.Instance;
    private IGameText scoreText;
    private IGameText levelText;

    public Transform Parent { get; set; }
    public IParticleSpawner ParticleSpawner
    {
        get
        {
            return particleSpawner;
        }
        set
        {
            particleSpawner = value;
        }
    }
    public IBlockColorRepository BlockColorRepository
    {
        get
        {
            return blockColorRepository;
        }
        set
        {
            blockColorRepository = value;
        }
    }
    public IFloatingTextRenderer FloatingTextRenderer
    {
        get
        {
            return floatingTextRenderer;
        }
        set
        {
            floatingTextRenderer = value;
        }
    }

    public bool IsProduction { get { return isProduction; } }
    public bool IsPlayer { get { return isPlayer; } }
    public CPUMode CPUDifficulty { get { return cpuDifficulty; } }
    public float ScalePerBlock { get { return scalePerBlock; } }
    public int GridWidth { get { return gridWidth; } }
    public int GridHeight { get { return gridHeight; } }
    public Vector3 GridPosition { get { return playerGridPosition; } }
    public Coord BlockSpawnPoint { get { return blockPopPoint; } }
    public float BlockFallSpeed { get { return blockFallSpeed; } }
    public float BlockDeleteSpeed { get { return blockDeleteSpeed; } }
    public float WaitAfterDelete { get { return waitAfterDelete; } }
    public StockDisplayConfig[] StockPositions { get { return stockPositions; } }

    public Vector3 GetGridCenterPosition()
    {
        Vector3 gridPosition = new Vector3(0, 0, 1);
        float adjustedX = (float)GridWidth / 2;
        adjustedX -= 0.5f;

        float adjustedY = (float)GridHeight / 2;
        adjustedY -= 0.5f;

        gridPosition = new Vector3(gridPosition.x + adjustedX, gridPosition.y + adjustedY, gridPosition.z);

        return gridPosition;
    }

    public Vector3 GetGridScale()
    {
        Vector3 gridScale = new Vector3(GridWidth, GridHeight, 1f);
        return gridScale;
    }

    private IGameTextManager gameTextManager;

    public void  Initialize()
    {
        gameTextManager = new GameTextManager(gameTextComponents);
    }

    void OnDrawGizmos()
    {
        if (stockPositions == null || stockPositions.Length == 0)
        {
            return;
        }

        foreach (StockDisplayConfig config in stockPositions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(config.position + playerGridPosition, 0.5f);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(GetGridCenterPosition() + GridPosition, GetGridScale() + new Vector3(0, 0, -1));
    }

    public IGameText GetGameText(GameTextType type)
    {
        return gameTextManager.GetGameText(type);
    }
}
