using UnityEngine;
using System.Collections;

public class BlockColorRepository : MonoBehaviour, IBlockColorRepository {

    public Color[] colorsForBlocks;

    public Color GetColorForTheBlock(BlockType blockType)
    {
        if (colorsForBlocks.Length < (int)BlockType.AvailableBlocks)
        {
            Debug.Log("Colors are not to be set correctly!");
            return Color.white;
        }

        return colorsForBlocks[(int)blockType];
    }


    public Color GetColorForTheNumber(int number)
    {
        if (colorsForBlocks.Length < (int)BlockType.AvailableBlocks)
        {
            Debug.Log("Colors are not to be set correctly!");
            return Color.white;
        }

        return colorsForBlocks[number];
    }
}
