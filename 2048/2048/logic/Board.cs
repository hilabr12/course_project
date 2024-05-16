using System;
using System.Linq;
namespace _2048.logic
{
    public class Board
    {
        internal const int BOARD_SIZE_ROW = 4;
        internal const int BOARD_SIZE_COLUMN = 4;
        internal Cell[,] BoardPoints;
        public static readonly int[] RANDOM_VALUES_OPTIONS = { 2, 4 };
        Random rnd = new Random();


        public Board()
        {
            BoardPoints = InitializeBoard();
        }

         
        public int[,] InitializeBoard()
        {
            Random rnd = new Random();
            int firstRandomNewCellValue = RANDOM_VALUES_OPTIONS.ElementAt(rnd.Next(RANDOM_VALUES_OPTIONS.Length));
            int secondRandomNewCellValue = RANDOM_VALUES_OPTIONS.ElementAt(rnd.Next(RANDOM_VALUES_OPTIONS.Length));
            Cell[,] board = new Cell[BOARD_SIZE_ROW, BOARD_SIZE_COLUMN];
            

        }

    }
}
