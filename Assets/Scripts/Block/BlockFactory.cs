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
        return Create(null, setting, blockType, location);
    }
    public IBlock Create(Transform parent, ISetting setting, BlockType blockType, Coord location)
    {
        if ((int)blockType >= 0 && (int)blockType < 7)
        {
            IBlock block = new Block(location);
            block.SetBlockType(blockType);
            if (parent == null)
            {
                DoAttachView(setting.Parent, block, setting, blockType, location);
            }
            else
            {
                DoAttachView(parent, block, setting, blockType, location);
            }
            return block;
        }
        else
        {
            Debug.Log("Not invalid blocktype is given!");
        }

        return null;
    }

    void DoAttachView(Transform parent, IBlock block, ISetting setting, BlockType blockType, Coord location)
    {
        if (_blockViewSpawner == null) return;

        block.AttachView(_blockViewSpawner.Spawn(parent, setting, (int)blockType, location));
    }
}
