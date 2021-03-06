﻿using UnityEngine;
using System.Collections;

public class TestSetting : ISetting 
{
    private bool isProduction = false;
    private bool isPlayer = true;
    private CPUMode cpuDifficulty = CPUMode.Easy;
    private float scalePerBlock = 1f;
    private int gridWidth = 7;
    private int gridHeight = 14;
    private Vector3 playerGridPosition = Vector3.zero;
    private StockDisplayConfig[] stockPositions = new StockDisplayConfig[] 
    {
        new StockDisplayConfig() { position = new Vector3(2, 0, 0) },
        new StockDisplayConfig() { position = new Vector3(2, 2, 0) },
    };

    private Coord blockPopPoint = new Coord(3, 13);
    private float blockFallSpeed = 10f;
    private float blockDeleteSpeed = 10f;
    private float waitAfterDelete = 0;
    private IParticleSpawner particleSpawner = NullParticleSpawner.Instance;
    private IBlockColorRepository blockColorRepository = NullBlockColorRepository.Instance;
    private IFloatingTextRenderer floatingTextRenderer = NullFloatingTextRenderer.Instance;

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

    private System.Random random;
    public System.Random Random
    {
        get
        {
            if (random == null) random = new System.Random(100);
            return random;
        }
        set
        {
            random = value;
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
        return Vector3.zero;
    }

    public Vector3 GetGridScale()
    {
        return Vector3.zero;
    }

    private static ISetting instance;

    private static ISetting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TestSetting();
            }
            return instance;
        }
    }

    public static ISetting Get()
    {
        return Instance;
    }

    private TestSetting() { }

    public IGameText GetGameText(GameTextType type)
    {
        return NullGameText.Instance;
    }
}
