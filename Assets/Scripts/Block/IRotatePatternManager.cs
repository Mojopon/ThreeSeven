using UnityEngine;
using System.Collections;

public interface IRotatePatternManager : IRotatePatternNumber
{
    Coord[] GetCurrentPattern();
    Coord[] GetRotatedPattern(RotateDirection rotateDirection);
}
