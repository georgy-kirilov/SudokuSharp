using System;

namespace SudokuSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudokuGrid = new SudokuGrid(4, 2);
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
                "210043",
                "000000",
                "006200",
                "003400",
                "000000",
                "340056",
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

            SudokuInput(sudokuGrid, fourByFourRawGrid);
            
            sudokuGrid.SolveSudoku();
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
