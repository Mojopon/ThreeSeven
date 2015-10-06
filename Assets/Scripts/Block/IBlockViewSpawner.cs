using UnityEngine;
using System.Collections;

public interface IBlockViewSpawner 
{
    IBlockView Spawn(ISetting setting, int number, Coord location);   
}
