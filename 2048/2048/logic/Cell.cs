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

        public bool IsWinning()
        {
            return Value == WinningCell;
        }
        public bool IsEmpty()
        {
            return Value == EmptyCell;
        }


        public void MakeEmpty()
        {
            Value = EmptyCell;
        }

        public void MergeValue()
        {
            Value *= 2;
        }

    }
}
