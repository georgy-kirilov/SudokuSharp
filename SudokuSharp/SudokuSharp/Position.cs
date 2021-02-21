namespace SudokuSharp
{
    public class Position
    {
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public Position NextHorizontal(int gridHeight, int gridWidth)
        {
            int column = this.Column + 1;
            int row = this.Row;
            if (column >= gridWidth)
            {
                column = 0;
                row++;
                if (row >= gridHeight)
                {
                    return null;
                }
            }
            return new Position(row, column);
        }
    }
}
