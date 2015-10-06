using UnityEngine;
using System.Collections;

public interface IMovableBlock : IUpdatable {

    bool IsOnMove { get; }
    Vector3 Offset { get; set; }

    void MoveTo(Vector2 position);
    void SetPosition(Vector2 position);
}
