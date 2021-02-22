using System;

namespace SudokuSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudokuGrid = new SudokuGrid(21, 3);
            var rawNineByNineGrid = new string[]
            {
                "003020600",
                "900305001",
                "001806400",
                "008102900",
                "700000008",
                "006708200",
                "002609500",
                "800203009",
                "005010300",
            };

            var sixBySixRawGrid = new string[]
            {
                "001000",
                "000600",
                "100030",
                "040002",
                "002000",
                "000200",
            };

            var sixBySixRawGrid2 = new string[]
            {
                "000000",
                "000000",
                "000000",
                "000000",
                "000000",
                "000000",
            };

            var eightByEight = new string[]
            {
                "00000000",
                "00000000",
                "00000000",
                "00000000",
                "00000000",
                "00000000",
                "00000000",
                "00000000",
            };

            var twelveByTwelve = new string[]
            {
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
                "000000000000",
            };

            var fourByFourRawGrid = new string[]
            {
                "3000",
                "2003",
                "0340",
                "4030",
            };

            //"3124",
            //"2413",
            //"1342",
            //"4231",

            var largeGrid = new int[20, 20];

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    sudokuGrid[i, j] = 0;
                }
            }

            var emptyRawGrid = new string[]
            {
                "003000000",
                "000000000",
                "000000000",
                "000000040",
                "000000000",
                "000600000",
                "000600000",
                "000000000",
                "000000050",
            };
            
            sudokuGrid.Solve();
            Console.WriteLine(sudokuGrid);
        }

        static void SudokuInput(SudokuGrid sudokuGrid, string[] rawGrid)
        {
            for (int i = 0; i < rawGrid.Length; i++)
            {
                for (int j = 0; j < rawGrid[i].Length; j++)
                {
                    sudokuGrid[i, j] = rawGrid[i][j] - '0';
                }
            }
        }
    }
}
