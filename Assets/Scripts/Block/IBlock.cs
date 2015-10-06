using UnityEngine;
using System.Collections;

public interface IBlock : IMovableBlock, IDeletableBlock, ISoundPlayer
{
    Coord Location { get; }
    Coord OriginalLocation { get; set; }
    BlockType BlockType { get; set; }
    int Number { get; }
    Vector2 WorldPosition { get; }

    void AttachView(IBlockView view);
    void SetLocation(Coord location);
    void MoveToLocation(Coord location);
    void OnFix();
    void StartDeleting();
    void DeleteImmediate();
}
