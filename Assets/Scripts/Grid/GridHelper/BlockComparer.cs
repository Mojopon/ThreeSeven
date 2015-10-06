using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class BlockComparer 
{
    public static List<IBlock> Compare(IBlock[,] grid)
    {
        var blocksToDelete = new HashSet<IBlock>();
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                StartComparing(grid, blocksToDelete, x, y);
            }
        }

        return blocksToDelete.ToList();
    }

    static bool StartComparing(IBlock[,] grid, HashSet<IBlock> toDelete, int x, int y)
    {
        bool itsTrue = false;

        if (grid[x, y] == null) return false;

        if (grid[x, y].Number != 7 && grid[x, y].Number != -1)
        {
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Up, grid[x, y].Number) == true ? true : itsTrue;
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Down, grid[x, y].Number) == true ? true : itsTrue;
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Left, grid[x, y].Number) == true ? true : itsTrue;
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Right, grid[x, y].Number) == true ? true : itsTrue;
        }
        else if (grid[x, y].Number == 7)
        {
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Up, 1) == true ? true : itsTrue;
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Down, 1) == true ? true : itsTrue;
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Left, 1) == true ? true : itsTrue;
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Right, 1) == true ? true : itsTrue;
        }

        return itsTrue;
    }

    static bool CompareNumbers(IBlock[,] grid, HashSet<IBlock> toDelete, Coord location, Direction direction, int num)
    {
        Coord check = location + direction.ToCoord();
        int x = (int)check.X;
        int y = (int)check.Y;
        int Width = grid.GetLength(0);
        int Height = grid.GetLength(1);

        if (x < 0 || y < 0 || x >= Width || y >= Height || grid[x, y] == null ||
            grid[x, y].Number == 7 ||
            grid[x, y].Number == -1)
        {
            return false;
        }

        num += grid[x, y].Number;

        if (num == 7)
        {
            toDelete.Add(grid[x, y]);
            return true;
        }

        if (CompareNumbers(grid, toDelete, check, direction, num))
        {
            toDelete.Add(grid[x, y]);
            return true;
        }

        return false;
    }

    static bool CompareSevens(IBlock[,] grid, HashSet<IBlock> toDelete, Coord location, Direction direction, int count)
    {
        Coord check = location + direction.ToCoord();
        int x = (int)check.X;
        int y = (int)check.Y;
        int Width = grid.GetLength(0);
        int Height = grid.GetLength(1);

        if (x < 0 || y < 0 || x >= Width || y >= Height || grid[x, y] == null ||
            grid[x, y].Number != 7)
        {
            return false;
        }

        count++;

        if (count >= 3)
        {
            toDelete.Add(grid[x, y]);
            return true;
        }

        if (CompareSevens(grid, toDelete, check, direction, count))
        {
            toDelete.Add(grid[x, y]);
            return true;
        }

        return false;
    }
}
