using UnityEngine;
using System.Collections;
using System;

public class Block : IBlock
{
    private IBlockView _view;

    public BlockType BlockType { get; private set; }
    public Coord Location { get; private set; }
    public Coord LocationInTheGroup { get; set; }
    public int Number
    {
        get
        {
            if ((int)BlockType > 6)
            {
                return -1;
            }

            return (int)BlockType+1;
        }
    }
    public Vector2 WorldPosition { get { return new Vector2(_view.WorldPosition.x, _view.WorldPosition.y); } }

    public Block()
    {
        _view = new NullBlockView();
    }

    public Block(Coord originalLocation) : this()
    {
        LocationInTheGroup = originalLocation;
    }

    public void AttachView(IBlockView view)
    {
        _view = view;
    }

    public void SetLocation(Coord location)
    {
        Location = location + LocationInTheGroup;
        UpdateBlockPosition();
    }

    public void Move(Coord velocity)
    {
        Location = velocity + LocationInTheGroup;
        MoveTo(velocity.ToVector2());
    }

    public void OnFix()
    {
        LocationInTheGroup = new Coord(0, 0);
    }

    public void StartDeleting()
    {
        _view.Delete();
    }

    public void DeleteImmediate()
    {
        _view.DeleteImmediate();
    }

    void UpdateBlockPosition()
    {
        SetPosition(new Vector2(Location.X, Location.Y));
    }

    #region IMovableBlock Group

    public bool IsOnMove { get { return _view.IsOnMove; } }
    public Vector3 Offset { get { return _view.Offset; } set { _view.Offset = value; } }

    public void MoveTo(Vector2 position)
    {
        _view.MoveTo(position);
    }

    public void SetPosition(Vector2 position)
    {
        _view.SetPosition(position);
    }

    #endregion

    #region IDeletableBlock Group

    public bool IsToDelete { get { return _view.IsToDelete; } }
    public bool IsDeleting { get { return _view.IsDeleting; } }

    #endregion

    #region IUpdatable Group

    public void OnUpdate()
    {
        _view.OnUpdate();
    }

    #endregion

    #region ISoundPlayer Group

    public void PlaySound(SoundName soundName)
    {
        _view.PlaySound(soundName);
    }

    public void SetBlockType(BlockType blockType)
    {
        BlockType = blockType;
    }

    #endregion
}
