using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class BlockComparer 
{
    public static List<T> Compare<T>(T[,] grid) where T : IBlockModel
    {
        var blocksToDelete = new HashSet<T>();

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                StartComparing(grid, blocksToDelete, x, y);
            }
        }

        return blocksToDelete.ToList();
    }

    static bool StartComparing<T>(T[,] grid, HashSet<T> toDelete, int x, int y) where T : IBlockModel
    {
        bool itsTrue = false;

        if (grid[x, y] == null) return false;

        var gridNumber = ((IBlockModel)grid[x, y]).Number;

        if (gridNumber != 7 && gridNumber != -1)
        {
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Up, gridNumber) == true ? true : itsTrue;
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Down, gridNumber) == true ? true : itsTrue;
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Left, gridNumber) == true ? true : itsTrue;
            itsTrue = CompareNumbers(grid, toDelete, new Coord(x, y), Direction.Right, gridNumber) == true ? true : itsTrue;
        }
        else if (gridNumber == 7)
        {
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Up, 1) == true ? true : itsTrue;
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Down, 1) == true ? true : itsTrue;
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Left, 1) == true ? true : itsTrue;
            itsTrue = CompareSevens(grid, toDelete, new Coord(x, y), Direction.Right, 1) == true ? true : itsTrue;
        }

        return itsTrue;
    }

    static bool CompareNumbers<T>(T[,] grid, HashSet<T> toDelete, Coord location, Direction direction, int num) where T : IBlockModel
    {
        Coord check = location + direction.ToCoord();
        int x = (int)check.X;
        int y = (int)check.Y;
        int Width = grid.GetLength(0);
        int Height = grid.GetLength(1);

        if (x < 0 || y < 0 || x >= Width || y >= Height || grid[x, y] == null) return false;

        var gridNumber = ((IBlockModel)grid[x, y]).Number;

        if (gridNumber == 7 || gridNumber == -1)
        {
            return false;
        }

        num += gridNumber;

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

    static bool CompareSevens<T>(T[,] grid, HashSet<T> toDelete, Coord location, Direction direction, int count) where T : IBlockModel
    {
        Coord check = location + direction.ToCoord();
        int x = (int)check.X;
        int y = (int)check.Y;
        int Width = grid.GetLength(0);
        int Height = grid.GetLength(1);

        if (x < 0 || y < 0 || x >= Width || y >= Height || grid[x, y] == null)
        {
            return false;
        }

        var gridNumber = ((IBlockModel)grid[x, y]).Number;
        if(gridNumber != 7)
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
