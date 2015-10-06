using UnityEngine;
using System.Collections;

public static class BlockDropper  
{
    public static IBlock[,] GetGridAfterDrop(IBlock[,] grid, out bool dropped)
    {
        int h = grid.GetLength(1);
        int w = grid.GetLength(0);

        IBlock[,] gridAfterDrop = new IBlock[w, h];
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                gridAfterDrop[x, y] = grid[x, y];
            }
        }

        bool dropping = true;
        dropped = false;

        int count = 0;
        while (dropping)
        {
            dropping = false;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (gridAfterDrop[x, y] != null && y - 1 >= 0)
                    {
                        if (gridAfterDrop[x, y - 1] == null)
                        {
                            gridAfterDrop[x, y - 1] = gridAfterDrop[x, y];
                            gridAfterDrop[x, y] = null;
                            dropping = true;
                            dropped = true;
                        }
                    }
                }
            }

            count++;
            if (count > 5000)
            {
                Debug.Log("Something weird is happenned!");
                return null;
            }
        }

        return gridAfterDrop;
    }
}
