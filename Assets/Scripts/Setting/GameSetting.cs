using UnityEngine;
using System.Collections;

public class GameSetting : MonoBehaviour, ISetting {

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
    private Vector3[] stockPositions;
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

    public float ScalePerBlock { get { return scalePerBlock; } }
    public int GridWidth { get { return gridWidth; } }
    public int GridHeight { get { return gridHeight; } }
    public Vector3 PlayerGridPosition { get { return playerGridPosition; } }
    public Coord BlockSpawnPoint { get { return blockPopPoint; } }
    public float BlockFallSpeed { get { return blockFallSpeed; } }
    public float BlockDeleteSpeed { get { return blockDeleteSpeed; } }
    public float WaitAfterDelete { get { return waitAfterDelete; } }
    public Vector3[] StockPositions { get { return stockPositions; } }

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

        foreach (Vector3 position in stockPositions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(position + playerGridPosition, 0.5f);
        }
    }

    public IGameText GetGameText(GameTextType type)
    {
        return gameTextManager.GetGameText(type);
    }
}
