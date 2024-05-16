using System;

namespace _2048.logic
{
    public class Board
    {
        internal const int BOARD_SIZE_ROW = 4;
        internal const int BOARD_SIZE_COLUMN = 4;
        internal int[,] BoardPoints;
        public static readonly int[] RANDOM_VALUES_OPTIONS = { 2, 4 };
        Random rnd = new Random();


        public Board()
        {
            BoardPoints = InitializeBoard();
        }


        public int[,] InitializeBoard()
        {


        }

    }
}
