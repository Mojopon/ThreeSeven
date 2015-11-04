using UnityEngine;
using System.Collections;

public interface IBlock : IControllableBlockView, IDeletableBlock, ISoundPlayer, IBlockModel, IControllableBlockModel
{
    Vector2 WorldPosition { get; }

    void AttachView(IBlockView view);
    void OnFix();
    void StartDeleting();
    void DeleteImmediate();
    void SetBlockType(BlockType blockType);
    void Move(Coord location);
}
