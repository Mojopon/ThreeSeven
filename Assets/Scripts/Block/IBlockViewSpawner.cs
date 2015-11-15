using UnityEngine;
using System.Collections;

public interface IBlockViewSpawner 
{
    IBlockView Spawn(Transform parent, ISetting setting, int number, Coord location);   
}
