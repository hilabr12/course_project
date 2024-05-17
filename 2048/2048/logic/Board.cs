using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
namespace _2048.logic
{
    public class Board
    {
        internal const int BOARD_SIZE_ROW = 4;

        internal const int BOARD_SIZE_COLUMN = 4;

        public const int EMPTY_CELL = 0;
        public Cell[,] Data { get; protected set; }

        public static readonly int[] RANDOM_VALUES_OPTIONS = { 2, 4 };

        
        public List<Cell> EmptyCells { get; protected set; }

        public Board()
        {
            Data = SetAllBoardValuesAsEmpty();
            EmptyCells = GetAllEmptyCells();
            InitializeBoard();
        }

        public List<Cell> GetAllEmptyCells()
        {
            List<Cell> emptyCells = new List<Cell>();
            for (int row = 0; row < BOARD_SIZE_ROW; row++)
            {
                for(int column = 0; column < BOARD_SIZE_COLUMN; column++)
                {
                    Cell currentCell = Data[row, column];
                    // if the current cell is empty then we add it to the list of empty cells
                    if (currentCell.IsEmpty())
                    {
                        emptyCells.Add(currentCell);
                    }
                }
            }
            return emptyCells;
        }

        public Cell[,] SetAllBoardValuesAsEmpty()
        {
            Cell[,] emptyBoard = new Cell[BOARD_SIZE_ROW,BOARD_SIZE_COLUMN];

            // setting all board values as EMPTY_CELL - 0
            for (int row = 0; row < BOARD_SIZE_ROW; row++)
            {
                for (int column = 0; column < BOARD_SIZE_COLUMN; column++)
                {
                    Cell currentCell = new Cell(EMPTY_CELL, row, column);
                    emptyBoard[row, column] = currentCell;
                }
            }
            return emptyBoard;
        }

        public void InitializeBoard()
        {
            AddRandomNumber();
            AddRandomNumber();
        }

        public void AddRandomNumber()
        {
            // getting random value
            int value = GetRandomCellValue();
            // getting a random place for the cell
            Cell cell = GetRandomEmptyCell();
            // setting the chosen random int value in the chosen random place
            SetRandomCellValue(cell, value);
            // removing the cell from the empty cells list 
            EmptyCells.Remove(cell);
        }



        public void SetRandomCellValue(Cell chosenRandomCell, int chosenRandomCellValue)
        {
            Data[chosenRandomCell.Row, chosenRandomCell.Column].Value = chosenRandomCellValue;
        }

        public int GetRandomCellValue()
        {
            Random rnd = new Random();
            int randomCellValue = RANDOM_VALUES_OPTIONS.ElementAt(rnd.Next(RANDOM_VALUES_OPTIONS.Length));
            return randomCellValue;
        }
        public Cell GetRandomEmptyCell()
        {
            Random rnd = new Random();
            // getting a random empty cell 
            Cell randomEmptyCell = EmptyCells.ElementAt(rnd.Next(EmptyCells.Count));
            return randomEmptyCell;
        }

        public bool AreThereAnyEmptyCells()
        {
            // returns true if there still empty cells , false if the game is over
            return EmptyCells.Count != 0;
        }

        public int Move(Direction direction)
        {
            int pointsEarned = 0;

            switch (direction)
            {
                case Direction.Up:
                    pointsEarned = MoveTilesLeft(-1, 0);
                    break;
                case Direction.Down:
                    pointsEarned = MoveTilesLeft(1, 0);
                    break;
                case Direction.Left:
                    pointsEarned = MoveTilesLeft(0, -1);
                    break;
                case Direction.Right:
                    pointsEarned = MoveTilesLeft(0, 1);
                    break;
            }
            
            return pointsEarned;
        }

        private int MoveTilesLeft(int rowLocationChange, int columnLocationChange)
        {
            int pointsEarned = 0;

            for (int row = 0; row < Data.GetLength(0); row++)
            {
                for (int column = BOARD_SIZE_COLUMN-1;  column >= 0 ; column--)
                {
                    if (!Data[row, column].IsEmpty()) 
                    {
                        int newRow = row;
                        int newColumn = column;
                        int counterMoves = 0;
                        // Looping until the next checked cell is valid and either empty or equal to the current cell
                        while (IsValidPosition(newRow + rowLocationChange, newColumn + columnLocationChange) &&
                               (Data[newRow + rowLocationChange, newColumn + columnLocationChange].IsEmpty() ||
                                Data[newRow + rowLocationChange, newColumn + columnLocationChange].Value == Data[row, column].Value))
                        {
                            newRow += rowLocationChange;
                            newColumn += columnLocationChange;
                            counterMoves++;
                            if(Data[newRow, newColumn].Value == Data[row, column].Value)
                            {
                                break;
                            }

                        }

                        pointsEarned += MoveTileAndMerge(row, column, newRow, newColumn);
                        column -= counterMoves;
                    }
                }
            }
            // adding new random cell
            AddRandomNumber();
            // after making changes in the board, update the emptyCells list 
            EmptyCells = GetAllEmptyCells();
            return pointsEarned;
        }


        private int MoveTileAndMerge(int row, int column, int newRow, int newColumn)
        {

            int pointsEarned = 0;

            // If the new position is different from the current position
            if (newRow != row || newColumn != column)
            {
                //Merging if the value of the new cell is equal to the value of the current cell
                if (Data[newRow, newColumn].Value == Data[row, column].Value)
                {
                    Data[newRow, newColumn].MergeValue();
                    Data[row, column].MakeEmpty();
                    pointsEarned += Data[newRow, newColumn].Value;
                }
                else
                {
                    // if the values are not equal
                    // moving the current cell to the new position
                    Data[newRow, newColumn].Value = Data[row, column].Value;
                    Data[row, column].MakeEmpty();
                }
            }
            return pointsEarned;
        }

        public bool IsValidPosition(int row, int column)
        {
            return row >= 0 && row < BOARD_SIZE_ROW && column >= 0 && column < BOARD_SIZE_COLUMN;
        }
        public bool IsThereAWinningCell()
        {
            for (int row = 0; row < Data.GetLength(0); row++)
            {
                for (int column = 0; column < Data.GetLength(1); column++)
                {
                    // if a cell is equal to 2048
                    if (Data[row, column].IsWinning())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
