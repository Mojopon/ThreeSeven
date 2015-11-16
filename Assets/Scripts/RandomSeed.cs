using UnityEngine;
using System.Collections;

public static class RandomSeed {
    private static int seed;
    public static int Get
    {
        get
        {
            if(seed == 0)
            {
                seed = Random.Range(0, 10000);
            }
            return seed;
        }
        set
        {
            seed = value;
        }
    }
}
