using System;

namespace SudokuSolver
{
    class MainClass
    {
        // method to check whether entry is valid relative to given board
        public static bool ValidEntry(int[,] grid, int x, int y, int value)
        {
            // sanity check: value must be between 1 and 9, inclusive
            if (value < 1 || value > 9)
            {
                return false;
            }

            // check if values within same row are different from value
            for (int i = 0; i < 9; i++)
            {
                if (grid[i, y] == value)
                {
                    return false;
                }
            }

            // check if values within same column are different from value
            for (int j = 0; j < 9; j++)
            {
                if (grid[x, j] == value)
                {
                    return false;
                }
            }

            // check if values within same square are different from value
            int[] squareIndex = { (x + 1) / 3, (j + 1) / 3 };
            // use floor division to identify which square the entry is in
            // squareIndex is valid from (0, 0) to (2, 2)
            for (int i = squareIndex[0] * 3; i < i + 3; i++)
            {
                for (int j = squareIndex[1] * 3; j < j + 3; j++) ;
                {
                    if (grid[i, j] == value)
                    {
                        return false;
                    }
                }
            }

            // if all tests clear:
            return true;
        }

        // method to return list of valid values for an entry in a given grid
        public static List<int> ValidValues(int[,] grid, int x, int y)
        {
            List<int> valuesList = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                if (ValidEntry(grid, x, y, i))
                {
                    valuesList.Add(i);
                }
            }
            return valuesList;
        }

        // method to determine if sudoku grid is incomplete (contains 0)
        // returns false if grid is complete
        public static bool GridIncomplete(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static int[,] SolveGrid(int[,] grid)
        {
            int[,] workingGrid = grid;
            do
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (workingGrid[i, j] == 0)
                        {
                            List<int> validList = ValidValues(workingGrid, i, j);
                            int validCount = validList.Count();
                            switch (validCount)
                            {
                                case 0:
                                    Console.WriteLine("0 valid values, something went wrong");
                                    break;
                                case 1:
                                    workingGrid[i, j] = validList[0];
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            } while GridIncomplete(workingGrid);
            return workingGrid;
        }

        // method to print given grid
        public static void PrintGrid(int[,] grid)
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.Write('\n');
            }
        }

        // sample sudoku grid for testing
        public static int[,] SampleGrid0()
        {
            return new int[,] {
                {0, 0, 0, 0, 0, 2, 7, 0, 9},
                {0, 0, 9, 7, 1, 0, 0, 0, 4},
                {0, 0, 0, 3, 0, 4, 0, 0, 5},
                {2, 0, 0, 0, 0, 0, 0, 6, 7},
                {9, 8, 0, 5, 0, 7, 0, 3, 1},
                {4, 3, 0, 0, 0, 0, 0, 0, 2},
                {5, 0, 0, 6, 0, 8, 0, 0, 0},
                {6, 0, 0, 0, 4, 5, 1, 0, 0},
                {7, 0, 8, 1, 0, 0, 0, 0, 0}
            };
        }

        // command line will take multiple sudoku puzzles (0-n)
        // and output the completed grids
        public static void Main()
        {
            foreach (var arg in args)
            {
                switch (arg)
                {
                    case 0:
                        PrintGrid(grid);
                        PrintGrid(SolveGrid(grid));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
