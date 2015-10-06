using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockPattern : IBlockPattern
{
    private BlockType[] _blockTypes;
    private BlockPattern(BlockType[] blockTypes) 
    {
        _blockTypes = blockTypes;
    }

    public static BlockPattern CreateFromNumbers(params int[] numbers)
    {
        BlockType[] blockTypes = new BlockType[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            blockTypes[i] = (BlockType)numbers[i] - 1;
        }

        return new BlockPattern(blockTypes);
    }


    public BlockType[] Types
    {
        get { return _blockTypes; }
    }
}
