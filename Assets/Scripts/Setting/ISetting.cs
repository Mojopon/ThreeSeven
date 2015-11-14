using UnityEngine;
using System.Collections;
using UniRx;

public interface ISetting : IGameTextManager
{
    Transform Parent { get; set; }
    IParticleSpawner ParticleSpawner { get; set; }
    IBlockColorRepository BlockColorRepository { get; set; }
    IFloatingTextRenderer FloatingTextRenderer { get; set; }

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
    Vector3[] StockPositions { get; }

    Vector3 GetGridCenterPosition();
    Vector3 GetGridScale();
}
