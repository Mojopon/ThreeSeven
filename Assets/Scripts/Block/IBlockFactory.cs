﻿using UnityEngine;
using System.Collections;

public interface IBlockFactory 
{
    IBlock Create(ISetting setting, BlockType blockType, Coord location);
    IBlock Create(Transform parent, ISetting setting, BlockType blockType, Coord location);
}
