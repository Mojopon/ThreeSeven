using UnityEngine;
using System.Collections;

public interface IBlockView : IMovableBlock, IDeletableBlock, ISoundPlayer
{
    Color Color { get; set; }
    Vector3 Position { get; set; }
    Vector3 WorldPosition { get; }
    ISetting Setting { get; set; }
    void Delete();
    void DeleteImmediate();
}
