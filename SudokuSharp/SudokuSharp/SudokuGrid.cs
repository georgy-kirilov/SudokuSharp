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
            this.Size = sideSize;
            this.subSquareWidth = subSquareWidth;
            this.subSquareHeight = sideSize / subSquareWidth;
            this.passedPositions = new Stack<Position>();
        }

        public int Size { get; }

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

        public void Solve()
        {
            Position currentPosition = new Position(0, 0);
            int startNumber = 1;

            while (currentPosition != null)
            {
                if (this[currentPosition] == 0)
                {
                    bool foundPlacableNumber = false;

                    for (int number = startNumber; number <= this.Size; number++)
                    {
                        bool isNumberPlacable = this.IsRowFreeOf(number, currentPosition.Row) && this.IsColumnFreeOf(number, currentPosition.Column) && this.IsSquareFreeOf(number, currentPosition);

                        if (isNumberPlacable)
                        {
                            this[currentPosition] = number;
                            this.passedPositions.Push(currentPosition);
                            startNumber = 1;
                            foundPlacableNumber = true;
                            break;
                        }
                    }

                    if (!foundPlacableNumber)
                    {
                        currentPosition = this.passedPositions.Pop();
                        startNumber = this[currentPosition] + 1;
                        this[currentPosition] = 0;
                    }
                }
                else
                {
                    currentPosition = currentPosition.NextHorizontal(this.Size, this.Size);
                    startNumber = 1;
                }
            }
        }

        public override string ToString()
        {
            var gridBuilder = new StringBuilder();
            string rowSeparationLine = "";
            int subSquareRealWidth = 2 * this.subSquareWidth + 3;
            int nextPlusIndex = subSquareRealWidth;
            int end = this.Size / this.subSquareWidth * (2 * this.subSquareWidth + 1) + this.Size / this.subSquareWidth + 1;

            for (int column = 1; column <= end; column++)
            {
                if (column == end || column == 1)
                {
                    rowSeparationLine += "+";
                }
                else if (column == nextPlusIndex)
                {
                    rowSeparationLine += "+";
                    nextPlusIndex += subSquareRealWidth - 1;
                }
                else
                {
                    rowSeparationLine += "-";
                }
            }

            for (int row = 0; row < this.Size; row++)
            {
                if (row == 0)
                {
                    gridBuilder.AppendLine(rowSeparationLine);
                }

                for (int column = 0; column < this.Size; column++)
                {
                    if (column == 0)
                    {
                        gridBuilder.Append("| ");
                    }

                    gridBuilder.Append($"{this[row, column]} ");

                    if (column == this.Size - 1)
                    {
                        gridBuilder.Append("|");
                    }
                    else if ((column + 1) % this.subSquareWidth == 0 && column < this.Size - 1)
                    {
                        gridBuilder.Append("| ");
                    }
                }

                if ((row + 1) % this.subSquareHeight == 0)
                {
                    gridBuilder.AppendLine("\n" + rowSeparationLine);
                }
                else
                {
                    gridBuilder.AppendLine();

                }
            }
            return gridBuilder.ToString().TrimEnd();
        }

        private bool IsRowFreeOf(int number, int row)
        {
            for (int column = 0; column < this.Size; column++)
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
            for (int row = 0; row < this.Size; row++)
            {
                if (this[row, column] == number)
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
