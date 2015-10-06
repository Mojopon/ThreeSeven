using UnityEngine;
using System.Collections;

public class NullBlockView : IBlockView {

    public NullBlockView() { }

    public Vector3 Offset { get; set; }
    public Color Color { get; set; }
    public Vector3 Position { get; set; }
    public ISetting Setting { get; set; }
    public Vector3 WorldPosition { get { return Position; } }

    public void Delete() 
    {
        isToDelete = true;
    }

    public void DeleteImmediate()
    {

    }

    private bool isToDelete = false;
    public bool IsToDelete { get { return isToDelete; } }
    public bool IsOnMove { get { return false; } }
    public void MoveTo(Vector2 position) { }
    public void SetPosition(Vector2 position) { }

    public bool IsDeleting { get { return false; } }

    public void OnUpdate() { }

    public void PlaySound(SoundName soundName)
    {
    }
}
