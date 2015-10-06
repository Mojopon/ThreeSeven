using UnityEngine;
using System.Collections;
using UniRx;

public interface ISetting : IGameTextManager
{
    Transform Parent { get; set; }
    IParticleSpawner ParticleSpawner { get; set; }
    IBlockColorRepository BlockColorRepository { get; set; }
    IFloatingTextRenderer FloatingTextRenderer { get; set; }

    float ScalePerBlock { get; }
    int GridWidth { get; }
    int GridHeight { get; }
    Vector3 PlayerGridPosition { get; }
    float BlockFallSpeed { get; }
    float BlockDeleteSpeed { get; }
    float WaitAfterDelete { get; }
    Coord BlockSpawnPoint { get; }
    Vector3[] StockPositions { get; }
}
