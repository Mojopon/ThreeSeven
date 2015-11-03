using UnityEngine;
using System.Collections;

public interface IBlock : IMovableBlock, IDeletableBlock, ISoundPlayer, IBlockModel
{
    Coord Location { get; }
    Coord OriginalLocation { get; set; }
    Vector2 WorldPosition { get; }

    void AttachView(IBlockView view);
    void SetLocation(Coord location);
    void MoveToLocation(Coord location);
    void OnFix();
    void StartDeleting();
    void DeleteImmediate();
    void SetBlockType(BlockType blockType);
}
