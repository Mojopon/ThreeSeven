using UnityEngine;
using System.Collections;

public interface IDeletableBlock : IUpdatable
{
    bool IsToDelete { get; }
    bool IsDeleting { get; }
}
