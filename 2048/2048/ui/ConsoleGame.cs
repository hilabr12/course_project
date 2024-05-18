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
        Console.WriteLine("Welcome to 2048 Game!");
        Console.WriteLine();
        Cell[,] boardData = _game.Board.Data;
        for (int row = 0; row < Board.BoardSizeRow; row++)

        {
            for (int col = 0; col < Board.BoardSizeColumn; col++)
            {
                Console.ForegroundColor = GetNumberColor((ulong)boardData[row, col].Value);
                Console.Write(string.Format("{0,8}", boardData[row, col].Value));
            }
            Console.WriteLine("");
        }
        Console.ResetColor(); // Reset console color to default


    }
    private static ConsoleColor GetNumberColor(ulong number)
    {
        switch (number)
        {
            case 0:
                return ConsoleColor.DarkGray;
            case 2:
                return ConsoleColor.Cyan;
            case 4:
                return ConsoleColor.Magenta;
            case 8:
                return ConsoleColor.Red;
            case 16:
                return ConsoleColor.Green;
            case 32:
                return ConsoleColor.Yellow;
            case 64:
                return ConsoleColor.Yellow;
            case 128:
                return ConsoleColor.DarkCyan;
            case 256:
                return ConsoleColor.Cyan;
            case 512:
                return ConsoleColor.DarkMagenta;
            case 1024:
                return ConsoleColor.Magenta;
            default:
                return ConsoleColor.Red;
        }
    }

    public Direction InputMove()
    {
        Console.WriteLine();
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

    public void Play()
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