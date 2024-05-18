namespace _2048.logic
{
    public class Cell
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool HasBeenMerged { get; set; }

        public const int EmptyCell = 0;
        public const int WinningCell = 2048;



        public Cell(int value, int row, int column)
        {
            Value = value;
            Row = row;
            Column = column;
            HasBeenMerged = false;
        }
        /// <summary>
        /// Checks if the cell's value equals the winning value (2048).
        /// </summary>
        /// <returns>True if the cell's value equals the winning value. otherwise, false.</returns>
        public bool IsWinning()
        {
            return Value == WinningCell;
        }
        /// <summary>
        /// Checks if the cell's value is equal to 0.
        /// </summary>
        /// <returns> True is the cell's value is empty. otherwise, false</returns>
        public bool IsEmpty()
        {
            return Value == EmptyCell;
        }

        /// <summary>
        /// Sets the cell's value to indicate it is empty (0).
        /// </summary>
        public void MakeEmpty()
        {
            Value = EmptyCell;
        }
        /// <summary>
        /// Doubles the value of the cell, simulating the merging of two identical tiles.
        /// </summary>
        public void MergeValue()
        {
            Value *= 2;
        }

    }
}
