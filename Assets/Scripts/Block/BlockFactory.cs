using UnityEngine;
using System.Collections;

public class BlockFactory : IBlockFactory 
{
    private IBlockViewSpawner _blockViewSpawner;

    public BlockFactory(IBlockViewSpawner blockViewSpawner) 
    {
        _blockViewSpawner = blockViewSpawner;
    }

    public IBlock Create(ISetting setting, BlockType blockType, Coord location)
    {
        if ((int)blockType >= 0 && (int)blockType < 7)
        {
            IBlock block = new Block(location);
            block.BlockType = blockType;
            DoAttachView(block, setting, blockType, location);
            return block;
        }
        else
        {
            Debug.Log("Not invalid blocktype is given!");
        }

        return null;
    }

    void DoAttachView(IBlock block, ISetting setting, BlockType blockType, Coord location)
    {
        if (_blockViewSpawner == null) return;

        block.AttachView(_blockViewSpawner.Spawn(setting, (int)blockType, location));
    }
}
