using _2048.logic;
using System;

public class ConsoleGame
{
    private Game _game { get; set; }
    public ConsoleGame()
    {
        _game = new Game();
    }

    public void PrintBoard()
    {
        Console.Clear();
        Cell[,] boardData = _game.Board.Data;
        for (int row = 0; row < Board.BOARD_SIZE_ROW; row++)

        {
            for (int col = 0; col < Board.BOARD_SIZE_COLUMN; col++)
            {
                Console.Write(boardData[row,col].ToString() + " ");
            }
            Console.WriteLine("");
        }
    }

    public Direction InputMove()
    {
        Console.WriteLine("Please Press an Arrow Key :");
        var key = Console.ReadKey().Key;
        Console.WriteLine(key);
        // checking valid key input
        while (key != ConsoleKey.DownArrow && key != ConsoleKey.UpArrow && key != ConsoleKey.LeftArrow && key != ConsoleKey.RightArrow)
        {
            key = Console.ReadKey().Key;
        }
        switch (key)
        {
            case ConsoleKey.UpArrow:
                return Direction.Up;
            case ConsoleKey.DownArrow:
                return Direction.Down;
            case ConsoleKey.LeftArrow:
                return Direction.Left;
        }
        return Direction.Right;
    }

    public void Run()
    {
        while (_game.Status == GameStatus.Idle)
        {
            PrintBoard();
            _game.Move(InputMove()); 
        }
        if (_game.Status == GameStatus.Win)
        {
            Console.WriteLine("Player Won!");
            Console.WriteLine("You earned {0} points!", _game.Points);

        }
        if (_game.Status == GameStatus.Lose)
        {
            PrintBoard();
            Console.WriteLine("Player Lost!");
            Console.WriteLine("You earned {0} points!", _game.Points);
        }
    }
}