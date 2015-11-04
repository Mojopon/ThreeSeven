using UnityEngine;
using System.Collections;

public interface IRotatePatternManager
{
    Coord[] GetCurrentPattern();
    Coord[] GetRotatedPattern(RotateDirection rotateDirection);
}
