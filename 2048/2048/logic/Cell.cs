using System;

namespace _2048.logic
{
    public class Cell
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        internal const int BOARD_SIZE_ROW = 4;

        internal const int BOARD_SIZE_COLUMN = 4;

        public const int EMPTY_CELL = 0;

        public const int WINNING_CELL = 2048;

        public static readonly int[] RANDOM_VALUES_OPTIONS = { 2, 4 };


        public Cell(int value, int row, int column)
        {
            Value = value;
            Row = row;
            Column = column;
        }

        public bool IsWinning()
        {
            return Value == WINNING_CELL;
        }
        public bool IsEmpty()
        {
            return Value == EMPTY_CELL;
        }
        
        public bool IsValidPosition()
        {
            return Row >= 0 && Row < BOARD_SIZE_ROW && Column >= 0 && Column < BOARD_SIZE_COLUMN;
        }
        
        public void MakeEmpty()
        {
            Value = EMPTY_CELL;
        }

        public void MergeValue()
        {
            Value *= 2;
        }

    }
}
