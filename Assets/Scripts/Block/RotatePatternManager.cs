using UnityEngine;
using System.Collections.Generic;

public class RotatePatternManager : IRotatePatternManager
{
    public int CurrentPattern { get; private set; }

    private List<Coord[]> _patterns;
    public RotatePatternManager(List<Coord[]> patterns)
    {
        CurrentPattern = 0;
        _patterns = patterns;
    }

    public Coord[] GetCurrentPattern()
    {
        return _patterns[CurrentPattern];
    }

    public Coord[] GetRotatedPattern(RotateDirection rotateDirection)
    {
        switch (rotateDirection)
        {
            case RotateDirection.Clockwise:
                {
                    CurrentPattern++;
                    if (CurrentPattern >= _patterns.Count) CurrentPattern = 0;
                }
                break;
            case RotateDirection.Counterclockwise:
                {
                    CurrentPattern--;
                    if (CurrentPattern < 0) CurrentPattern = _patterns.Count - 1;
                }
                break;
        }

        return GetCurrentPattern();
    }
}
