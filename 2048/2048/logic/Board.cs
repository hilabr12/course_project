﻿using System;
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
        internal const int BoardSizeRow = 4;

        internal const int BoardSizeColumn = 4;

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
            for (int row = 0; row < BoardSizeColumn; row++)
            {
                for (int column = 0; column < BoardSizeColumn; column++)
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
            Cell[,] emptyBoard = new Cell[BoardSizeColumn, BoardSizeColumn];

            // setting all board values as EMPTY_CELL - 0
            for (int row = 0; row < BoardSizeColumn; row++)
            {
                for (int column = 0; column < BoardSizeColumn; column++)
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
        public int Move(Direction direction)
        {
            int pointsEarned = 0;

            switch (direction)
            {
                case Direction.Up:
                    pointsEarned = MoveTilesUp();
                    break;
                case Direction.Down:
                    pointsEarned = MoveTilesDown();
                    break;
                case Direction.Left:
                    pointsEarned = MoveTilesLeft();
                    break;
                case Direction.Right:
                    pointsEarned = MoveTilesRight();
                    break;
            }

            return pointsEarned;
        }


        private int MoveTilesUp()
        {

            int pointsEarned = 0;

            for (int col = 0; col < BoardSizeColumn; col++)
            {
                for (int row = 1; row < BoardSizeRow; row++)
                {
                    if (!Data[row, col].IsEmpty())
                    {
                        int currentRow = row;
                        while (currentRow > 0 
                            && (Data[currentRow - 1, col].IsEmpty() 
                            || (Data[currentRow - 1, col].Value == Data[row, col].Value 
                            && !Data[currentRow - 1, col].HasBeenMerged)))
                        {
                            currentRow--;
                        }
                        pointsEarned += MoveTileAndMerge(row, col, currentRow, col);
                    }
                }
            }

            AfterMergingTasks();
            return pointsEarned;
        }

        private int MoveTilesDown()
        {
            int pointsEarned = 0;

            for (int col = 0; col < BoardSizeColumn; col++)
            {
                for (int row = BoardSizeRow - 2; row >= 0; row--)
                {
                    if (!Data[row, col].IsEmpty())
                    {
                        int currentRow = row;
                        while (currentRow < BoardSizeRow - 1 
                            && (Data[currentRow + 1, col].IsEmpty() 
                            || (Data[currentRow + 1, col].Value == Data[row, col].Value 
                            && !Data[currentRow + 1, col].HasBeenMerged)))
                        {
                            currentRow++;
                        }
                        pointsEarned += MoveTileAndMerge(row, col, currentRow, col);
                    }
                }
            }

            AfterMergingTasks();
            return pointsEarned;
        }
        private int MoveTilesRight()
        {
            int pointsEarned = 0;

            for (int row = 0; row < BoardSizeRow; row++)
            {
                for (int col = BoardSizeColumn - 2; col >= 0; col--)
                {
                    if (!Data[row, col].IsEmpty())
                    {
                        int currentCol = col;
                        while (currentCol < BoardSizeColumn - 1 
                            && (Data[row, currentCol + 1].IsEmpty() 
                            || (Data[row, currentCol + 1].Value == Data[row, col].Value 
                            && !Data[row, currentCol + 1].HasBeenMerged)))
                        {
                            currentCol++;
                        }
                        // Move and possibly merge the tile
                        pointsEarned += MoveTileAndMerge(row, col, row, currentCol);
                    }
                }
            }

            AfterMergingTasks();
            return pointsEarned;
        }

        private int MoveTilesLeft()
        {
            int pointsEarned = 0;

            for (int row = 0; row < Data.GetLength(0); row++)
            {
                for (int column = 1; column < Data.GetLength(1); column++)
                {
                    if (!Data[row, column].IsEmpty())
                    {
                        int newRow = row;
                        int newColumn = column;

                        // Find the valid empty position or the position with the same value
                        while (newColumn > 0 
                            && (Data[newRow, newColumn - 1].IsEmpty() 
                            || (Data[newRow, newColumn - 1].Value == Data[row, column].Value 
                            && !Data[newRow, newColumn - 1].HasBeenMerged)))
                        {
                            newColumn--;
                        }

                        // Move and possibly merge the tile
                        pointsEarned += MoveTileAndMerge(row, column, newRow, newColumn);
                    }
                }
            }

            AfterMergingTasks();

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

        public void AfterMergingTasks()
        {
            // Update the emptyCells list 
            EmptyCells = GetAllEmptyCells();
            //Add Random Number
            AddRandomNumber();
            // Reset Has Been Merged Flag For All Cells
            ResetHasBeenMergedFlagForAllCells();
        }


        public void ResetHasBeenMergedFlagForAllCells()
        {
            // Reset the HasBeenMerged flag for all cells
            for (int row = 0; row < Data.GetLength(0); row++)
            {
                for (int column = 0; column < Data.GetLength(1); column++)
                {
                    Data[row, column].HasBeenMerged = false;
                }
            }
        }

        
        

    }
}