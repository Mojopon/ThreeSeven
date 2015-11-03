using UnityEngine;
using System.Collections;

public interface IBlockModel
{ 
    BlockType BlockType { get; }
    int Number { get; }
}
