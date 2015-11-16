using UnityEngine;
using System.Collections;
using UniRx;

public interface ISetting : IGameTextManager
{
    bool IsProduction { get; }

    Transform Parent { get; set; }
    IParticleSpawner ParticleSpawner { get; set; }
    IBlockColorRepository BlockColorRepository { get; set; }
    IFloatingTextRenderer FloatingTextRenderer { get; set; }
    System.Random Random { get; set; }

    bool IsPlayer { get; }
    CPUMode CPUDifficulty { get; }
    float ScalePerBlock { get; }
    int GridWidth { get; }
    int GridHeight { get; }
    Vector3 GridPosition { get; }
    float BlockFallSpeed { get; }
    float BlockDeleteSpeed { get; }
    float WaitAfterDelete { get; }
    Coord BlockSpawnPoint { get; }
    StockDisplayConfig[] StockPositions { get; }

    Vector3 GetGridCenterPosition();
    Vector3 GetGridScale();
}
