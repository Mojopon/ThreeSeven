using UnityEngine;
using System.Collections;

public class NullBlockColorRepository : IBlockColorRepository {

    private static NullBlockColorRepository instance;
    public static NullBlockColorRepository Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NullBlockColorRepository();
            }

            return instance;
        }
    }

    private NullBlockColorRepository() { }

    public Color GetColorForTheBlock(BlockType blockType)
    {
        return Color.white;
    }

    public Color GetColorForTheNumber(int number)
    {
        return Color.white;
    }
}
