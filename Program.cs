using System;

namespace Island;
class Program
{
    static void Main(string[] args)
    {
        Game newGame = new Game();
        newGame.startMenu();
        newGame.runGame();
    }
}