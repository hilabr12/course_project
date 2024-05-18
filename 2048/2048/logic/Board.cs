using System;
using System.Collections.Generic;
using System.Linq;
namespace _2048.logic
{
    public class Board
    {
        public List<Cell> EmptyCells { get; protected set; }
        public Cell[,] Data { get; protected set; }
        public static readonly int[] RANDOM_VALUES_OPTIONS = { 2, 4 };

        internal const int BoardSizeRow = 4;
        internal const int BoardSizeColumn = 4;
        public const int EmptyCell = 0;

        public Board()
        {
            Data = SetAllBoardValuesAsEmpty();
            EmptyCells = GetAllEmptyCells();
            InitializeBoard();
        }
        /// <summary>
        /// Retrieves a list of all empty cells on the game board.
        /// </summary>
        /// <returns>A list containing all empty cells on the game board.</returns>
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
        /// <summary>
        /// Initializes a new game board with all cells set to empty.
        /// </summary>
        /// <returns>A 2D array representing the empty game board.</returns>
        public Cell[,] SetAllBoardValuesAsEmpty()
        {
            Cell[,] emptyBoard = new Cell[BoardSizeColumn, BoardSizeColumn];

            // setting all board values as EMPTY_CELL - 0
            for (int row = 0; row < BoardSizeColumn; row++)
            {
                for (int column = 0; column < BoardSizeColumn; column++)
                {
                    Cell currentCell = new Cell(EmptyCell, row, column);
                    emptyBoard[row, column] = currentCell;
                }
            }
            return emptyBoard;
        }
        /// <summary>
        /// Initializes the game board by adding two random numbers to it.
        /// </summary>
        public void InitializeBoard()
        {
            AddRandomNumber();
            AddRandomNumber();
        }
        /// <summary>
        /// Adds a random number to a randomly chosen empty cell on the game board.
        /// </summary>
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


        /// <summary>
        /// Sets the value of the specified cell on the game board to the given random cell value.
        /// </summary>
        /// <param name="chosenRandomCell">The cell where the value will be set.</param>
        /// <param name="chosenRandomCellValue">The value to set in the cell.</param>
        public void SetRandomCellValue(Cell chosenRandomCell, int chosenRandomCellValue)
        {
            Data[chosenRandomCell.Row, chosenRandomCell.Column].Value = chosenRandomCellValue;
        }
        /// <summary>
        /// Generates a random cell value from the available options.
        /// </summary>
        /// <returns>A randomly chosen cell value from the available options.</returns>
        public int GetRandomCellValue()
        {
            Random rnd = new Random();
            int randomCellValue = RANDOM_VALUES_OPTIONS.ElementAt(rnd.Next(RANDOM_VALUES_OPTIONS.Length));
            return randomCellValue;
        }
        /// <summary>
        /// Retrieves a random empty cell from the list of empty cells on the game board.
        /// </summary>
        /// <returns>A randomly chosen empty cell.</returns>
        public Cell GetRandomEmptyCell()
        {
            Random rnd = new Random();
            // getting a random empty cell 
            Cell randomEmptyCell = EmptyCells.ElementAt(rnd.Next(EmptyCells.Count));
            return randomEmptyCell;
        }
        /// <summary>
        /// Checks if there are any empty cells remaining on the game board.
        /// </summary>
        /// <returns>True if there are still empty cells; otherwise, false.</returns>
        public bool AreThereAnyEmptyCells()
        {
            // returns true if there still empty cells , false if the game is over
            return EmptyCells.Count != 0;
        }
        /// <summary>
        /// Checks if there is a cell on the game board with a value equal to the winning value (2048).
        /// </summary>
        /// <returns>True if there is a winning cell; otherwise, false.</returns>
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
        /// <summary>
        /// Moves the tiles on the game board in the specified direction and returns the points earned from the move.
        /// </summary>
        /// <param name="direction">The direction in which to move the tiles.</param>
        /// <returns>The points earned from the move.</returns>
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

        /// <summary>
        /// Moves and merges tiles upwards on the game board.
        /// </summary>
        /// <returns>The points earned from the move.</returns>
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
                        // move the tile upwards as long as the position above is empty or has the same value and hasn't been merged before
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
        /// <summary>
        /// Moves and merges tiles down on the game board.
        /// </summary>
        /// <returns>The points earned from the move.</returns>
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
                        // move the tile downwards as long as the position below is empty or has the same value and hasn't been merged before
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
        /// <summary>
        /// Moves and merges tiles right on the game board.
        /// </summary>
        /// <returns>The points earned from the move.</returns>
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
                        // move the tile to the right as long as the position to the right is empty or has the same value and hasn't been merged before
                        while (currentCol < BoardSizeColumn - 1
                            && (Data[row, currentCol + 1].IsEmpty()
                            || (Data[row, currentCol + 1].Value == Data[row, col].Value
                            && !Data[row, currentCol + 1].HasBeenMerged)))
                        {
                            currentCol++;
                        }
                        // move and possibly merge the tile
                        pointsEarned += MoveTileAndMerge(row, col, row, currentCol);
                    }
                }
            }

            AfterMergingTasks();
            return pointsEarned;
        }
        /// <summary>
        /// Moves and merges tiles left on the game board.
        /// </summary>
        /// <returns>The points earned from the move.</returns>
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
        /// <summary>
        /// Moves a tile to a new position and merges it with another cell if their values are equal.
        /// </summary>
        /// <param name="row">The row index of the current cell position.</param>
        /// <param name="column">The column index of the current cell position.</param>
        /// <param name="newRow">The row index of the new cell position.</param>
        /// <param name="newColumn">The column index of the new cell position.</param>
        /// <returns>The points earned from merging cells.</returns>
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
        /// <summary>
        /// Performs tasks after merging tiles, including updating the list of empty cells, adding a new random number, and resetting the "HasBeenMerged" flag for all cells.
        /// </summary>
        public void AfterMergingTasks()
        {
            // Update the emptyCells list 
            EmptyCells = GetAllEmptyCells();
            //Add Random Number
            AddRandomNumber();
            // Reset Has Been Merged Flag For All Cells
            ResetHasBeenMergedFlagForAllCells();
        }

        /// <summary>
        /// Resets the "HasBeenMerged" flag for all cells on the game board.
        /// </summary>
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