using UnityEngine;
using System.Collections.Generic;

public static class ScoreCalculator
{
    public static int Calculate(List<IBlockModel> blocks, int chains)
    {
        int scoreBase = 0;
        foreach (IBlockModel block in blocks)
        {
            scoreBase += block.Number;
        }

        int score = (scoreBase * 10) * chains;

        return score;
    }

    public static int Calculate<T>(List<T> blocks, int chains) where T : IBlockModel
    {
        List<IBlockModel> blockModels = new List<IBlockModel>();
        foreach(T block in blocks)
        {
            blockModels.Add(block);
        }

        return Calculate(blockModels, chains);
    }
}