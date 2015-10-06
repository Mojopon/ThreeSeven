using UnityEngine;
using System.Collections;

public interface IBlockColorRepository {

    Color GetColorForTheBlock(BlockType blockType);
    Color GetColorForTheNumber(int number);
}
