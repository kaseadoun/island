using System;

namespace Island;
class Program
{
    static void Main(string[] args)
    {
        Game newGame = new Game();
        newGame.startMenu();
        try
        {
            newGame.runGame();
        }
        catch (Exception e)
        {
            Console.WriteLine("The game was unable to run.");
            Console.WriteLine("Thanks for trying to play!");
        }
    }
}