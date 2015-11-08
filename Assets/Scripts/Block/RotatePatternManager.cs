using UnityEngine;
using System.Collections.Generic;

public class RotatePatternManager : IRotatePatternManager
{
    public int CurrentRotatePatternNumber { get; private set; }

    private List<Coord[]> _patterns;
    public RotatePatternManager(List<Coord[]> patterns)
    {
        CurrentRotatePatternNumber = 0;
        _patterns = patterns;
    }

    public RotatePatternManager(List<Coord[]> patterns, int initialNumber) : this(patterns)
    {
        CurrentRotatePatternNumber = initialNumber;
        if(CurrentRotatePatternNumber >= patterns.Count)
        {
            CurrentRotatePatternNumber = 0;
        }
    }

    public Coord[] GetCurrentPattern()
    {
        return _patterns[CurrentRotatePatternNumber];
    }

    public Coord[] GetRotatedPattern(RotateDirection rotateDirection)
    {
        switch (rotateDirection)
        {
            case RotateDirection.Clockwise:
                {
                    CurrentRotatePatternNumber++;
                    if (CurrentRotatePatternNumber >= _patterns.Count) CurrentRotatePatternNumber = 0;
                }
                break;
            case RotateDirection.Counterclockwise:
                {
                    CurrentRotatePatternNumber--;
                    if (CurrentRotatePatternNumber < 0) CurrentRotatePatternNumber = _patterns.Count - 1;
                }
                break;
        }

        return GetCurrentPattern();
    }
}
