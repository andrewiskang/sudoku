using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    class MainClass
    {
        // method to return all valid values for an entry given sudoku board
        public static List<int> ValidEntries(int[,] grid, int x, int y)
        {
            // initialize full list from 1-9 and remove values within same 3x3
            List<int> validList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // use floor division to identify which square the entry is in
            // squareIndex is valid from (0, 0) to (2, 2)
            int[] squareIndex = { x / 3, y / 3 };
            for (int i = squareIndex[0] * 3; i < squareIndex[0] * 3 + 3; i++)
            {
                for (int j = squareIndex[1] * 3; j < squareIndex[1] * 3 + 3; j++)
                {
                    if (grid[i, j] != 0)
                    {
                        validList.Remove(grid[i, j]);
                    }
                }
            }

            // now remove all entries within same row
            for (int i = 0; i < 9; i++)
            {
                if (grid[i, y] != 0)
                {
                    validList.Remove(grid[i, y]);
                }
            }

            // now remove all entries within same column
            for (int j = 0; j < 9; j++)
            {
                if (grid[x, j] != 0)
                {
                    validList.Remove(grid[x, j]);
                }
            }
            return validList;
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
                            List<int> validList = ValidEntries(workingGrid, i, j);
                            int validCount = validList.Count;
                            switch (validCount)
                            {
                                case 0:
                                    Console.WriteLine("0 valid values, something went wrong");
                                    break;
                                case 1:
                                    workingGrid[i, j] = validList[0];
                                    Console.WriteLine("added {0} at ({1}, {2})", workingGrid[i, j], i, j);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            } while (GridIncomplete(workingGrid));
            Console.Write('\n');
            return workingGrid;
        }

        // method to print given grid
        public static void PrintGrid(int[,] grid)
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    Console.Write("{0} ", grid[i, j]);
                }
                Console.Write('\n');
            }
        }

        // sample sudoku grids for testing
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
        public static int[,] SampleGrid1()
        {
            return new int[,] {
                {0, 0, 0, 7, 0, 1, 0, 0, 0},
                {4, 0, 9, 0, 6, 2, 3, 0, 0},
                {3, 0, 1, 0, 0, 0, 2, 6, 7},
                {6, 0, 0, 0, 0, 7, 0, 4, 0},
                {1, 0, 0, 4, 5, 8, 0, 0, 6},
                {0, 4, 0, 6, 0, 0, 0, 0, 2},
                {7, 6, 3, 0, 0, 0, 1, 0, 9},
                {0, 0, 5, 1, 9, 0, 4, 0, 3},
                {0, 0, 0, 5, 0, 3, 0, 0, 0}
            };
        }
        public static int[,] SampleGrid2()
        {
            return new int[,] {
                {0, 0, 0, 0, 2, 9, 5, 0, 6},
                {0, 0, 0, 8, 0, 0, 0, 0, 0},
                {0, 9, 0, 0, 0, 0, 8, 4, 0},
                {0, 0, 7, 0, 6, 0, 2, 0, 5},
                {0, 1, 0, 0, 0, 0, 0, 9, 0},
                {6, 0, 2, 0, 9, 0, 4, 0, 0},
                {0, 6, 1, 0, 0, 0, 0, 3, 0},
                {0, 0, 0, 0, 0, 6, 0, 0, 0},
                {4, 0, 3, 1, 8, 0, 0, 0, 0}
            };
        }

        // command line will take multiple sudoku puzzles (0-n)
        // and output the completed grids
        public static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "0":
                        Console.WriteLine("Starting grid:");
                        PrintGrid(SampleGrid0());
                        int[,] completedGrid = SolveGrid(SampleGrid0());
                        Console.WriteLine("Completed grid:");
                        PrintGrid(completedGrid);
                        Console.WriteLine('\n');
                        break;
                    case "1":
                        Console.WriteLine("Starting grid:");
                        PrintGrid(SampleGrid1());
                        completedGrid = SolveGrid(SampleGrid1());
                        Console.WriteLine("Completed grid:");
                        PrintGrid(completedGrid);
                        Console.WriteLine('\n');
                        break;
                    case "2":
                        Console.WriteLine("Starting grid:");
                        PrintGrid(SampleGrid2());
                        completedGrid = SolveGrid(SampleGrid2());
                        Console.WriteLine("Completed grid:");
                        PrintGrid(completedGrid);
                        Console.WriteLine('\n');
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
