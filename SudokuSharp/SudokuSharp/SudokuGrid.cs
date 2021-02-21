namespace SudokuSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SudokuGrid
    {
        private readonly int[,] grid;
        private int subSquareWidth;
        private int subSquareHeight;

        private readonly Stack<Position> passedPositions;

        public SudokuGrid(int sideSize, int subSquareWidth)
        {
            if (sideSize % subSquareWidth != 0)
            {
                throw new ArgumentException("Subsquare width should be divisible by the grid side size");
            }

            this.grid = new int[sideSize, sideSize];
            this.subSquareWidth = subSquareWidth;
            this.subSquareHeight = sideSize / subSquareWidth;
            this.passedPositions = new Stack<Position>();
        }

        public int Height => this.grid.GetLength(0);

        public int Width => this.grid.GetLength(1);

        public int this[int row, int column]
        {
            get => this.grid[row, column];
            set
            {
                this.grid[row, column] = value;
            }
        }

        public int this[Position position]
        {
            get => this[position.Row, position.Column];
            set
            {
                this[position.Row, position.Column] = value;
            }
        }

        public void SolveSudoku()
        {
            void Solve(Position currentPosition, int startNumber)
            {
                if (currentPosition == null)
                {
                    return;
                }

                if (this[currentPosition] == 0)
                {
                    for (int number = startNumber; number <= this.Height; number++)
                    {
                        bool isNumberPlacable = this.IsRowFreeOf(number, currentPosition.Row) && this.IsColumnFreeOf(number, currentPosition.Column) && this.IsSquareFreeOf(number, currentPosition);

                        if (isNumberPlacable)
                        {
                            this[currentPosition] = number;
                            this.passedPositions.Push(currentPosition);
                            
                            Solve(currentPosition.NextHorizontal(this.Height, this.Width), 1);
                            return;
                        }
                    }
                    
                    currentPosition = this.passedPositions.Pop();
                    startNumber = this[currentPosition] + 1;
                    this[currentPosition] = 0;
                    Solve(currentPosition, startNumber);
                    return;
                }
                if (currentPosition != null)
                {
                    Solve(currentPosition.NextHorizontal(this.Height, this.Width), 1);
                }
            }

            Solve(new Position(0, 0), 1);
        }

        public override string ToString()
        {
            var gridBuilder = new StringBuilder();
            for (int row = 0; row < this.Height; row++)
            {
                for (int column = 0; column < this.Width; column++)
                {
                    gridBuilder.Append($"{this[row, column]} ");
                }
                gridBuilder.AppendLine();
            }
            return gridBuilder.ToString().TrimEnd();
        }

        private bool IsRowFreeOf(int number, int row)
        {
            for (int column = 0; column < this.Width; column++)
            {
                if (this[row, column] == number)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsColumnFreeOf(int number, int column)
        {
            for (int row = 0; row < this.Height; row++)
            {
                if (this.grid[row, column] == number)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSquareFreeOf(int number, Position position)
        {
            int startRow = position.Row / this.subSquareHeight * this.subSquareHeight;
            int startColumn = position.Column / this.subSquareWidth * this.subSquareWidth;

            int endRow = startRow + this.subSquareHeight;
            int endColumn = startColumn + this.subSquareWidth;

            for (int row = startRow; row < endRow; row++)
            {
                for (int column = startColumn; column < endColumn; column++)
                {
                    if (this[row, column] == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
