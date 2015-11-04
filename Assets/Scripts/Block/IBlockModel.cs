using UnityEngine;
using System.Collections;

public interface IBlockModel : IControllableBlockModel
{ 
    BlockType BlockType { get; }
    int Number { get; }
}
