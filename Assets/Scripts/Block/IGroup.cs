using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGroup : ISoundPlayer
{
    Vector3 Offset { get; set; }
    void AddBlock(IBlock block);
    void SetLocation(Coord location);
    void SetPattern(List<Coord[]> patterns);
    void Move(Direction direction);
    void Rotate(RotateDirection rotateDirection);
    void Fix();
    Coord Location { get; }
    Coord[] ChildrenLocation { get; }
    IBlock[] Children { get; }
}
