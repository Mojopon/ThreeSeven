using UnityEngine;
using System.Collections.Generic;

public interface IGroupModel : IRotatePatternNumber
{
    Coord Location { get; }
    Coord[] ChildrenLocation { get; }
    List<Coord[]> GetPattern();

    void SetLocation(Coord location);
    void Move(Direction direction);
    void Rotate(RotateDirection rotateDirection);
}
