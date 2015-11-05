using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGroup : ISoundPlayer, IGroupModel
{
    Vector3 Offset { get; set; }
    void AddBlock(IBlock block);
    void Fix();
    IBlock[] Children { get; }
    void SetPattern(List<Coord[]> patterns);
}
