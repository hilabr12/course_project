using _2048.logic;
using System;
using System.Runtime.InteropServices;

namespace _2048
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleGame game = new ConsoleGame();
            game.Play();
        }
    }
}
