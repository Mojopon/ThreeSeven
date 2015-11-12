using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Group : IGroup
{
    private Vector3 _offset;
    public Vector3 Offset
    {
        get
        {
            return _offset;
        }
        set
        {
            _offset = value;
            foreach (IBlock block in blocks)
            {
                block.Offset = _offset;
            }
        }
    }

    private Coord _location;
    private List<IBlock> blocks;

    private int currentPattern = 0;
    private List<Coord[]> _patterns;

    private ISetting _setting;
    private IRotatePatternManager _rotatePatternManager;
    public int CurrentRotatePatternNumber { get
        {
            if(_rotatePatternManager == null)
            {
                return -1;
            }

            return _rotatePatternManager.CurrentRotatePatternNumber;
        }
    }

    public int RotationPatternNumber
    {
        get
        {
            if (_rotatePatternManager == null)
            {
                return -1;
            }

            return _rotatePatternManager.RotationPatternNumber;
        }
    }

    public Group(ISetting setting)
    {
        blocks = new List<IBlock>();
        _setting = setting;
    }

    public void SetLocation(Coord location)
    {
        _location = location;
        foreach (IBlock block in blocks)
        {
            block.SetLocation(location);
        }
    }

    public void SetPattern(List<Coord[]> patterns)
    {
        _patterns = patterns;
        _rotatePatternManager = new RotatePatternManager(_patterns);
    }

    public List<Coord[]> GetPattern()
    {
        return _patterns;
    }

    public void AddBlock(IBlock block)
    {
        blocks.Add(block);
    }

    public void Move(Direction direction)
    {
        Coord nextLocation = _location + direction.ToCoord();
        SetLocation(nextLocation);
    }

    public void Rotate(RotateDirection rotateDirection)
    {
        var currentPattern = _rotatePatternManager.GetRotatedPattern(rotateDirection);

        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].LocationInTheGroup = currentPattern[i];
        }

        SetLocation(Location);
    }

    public void Fix()
    {
        foreach (IBlock block in blocks)
        {
            block.OnFix();
        }
    }

    public Coord Location
    {
        get { return _location; }
    }

    public Coord[] ChildrenLocation
    {
        get
        {
            Coord[] coords = new Coord[blocks.Count];
            for(int i = 0; i < blocks.Count; i++)
            {
                coords[i] = blocks[i].Location;
            }

            return coords;
        }
    }

    public IBlock[] Children { get { return blocks.ToArray(); } }

    #region ISoundPlayer Group

    public void PlaySound(SoundName soundName)
    {
        foreach (IBlock block in blocks)
        {
            block.PlaySound(soundName);
        }
    }

    #endregion
}
