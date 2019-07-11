using System;

// For SOLID principles as outlined in the user stories, please see the Weather.cs file for Open/Closed, and see Store.cs for Single Responsibility

namespace LemonadeStand
{
    class Program
    {
        static int Main(string[] args)
        {
            int gameReturnVal;
            if (args.Length < 3)
            {
                Console.WriteLine("Error: please run from the handler application.");
                Console.ReadKey();
                gameReturnVal = 1;
            }
            else if (args.Length > 3)
            {
                Console.WriteLine("Error: please run from the handler application.");
                Console.ReadKey();
                gameReturnVal = 1;
            }
            else
            {
                if (args[2] == SeededData.key)
                {
                    int playerNum = Convert.ToInt32(args[0]);
                    Game game;

                    Console.WriteLine("Welcome to \"Lemonade Stand\"!");
                    Console.WriteLine("You start with $20 and need to get as much money as possible by running your own Lemonade Stand.");
                    UserInterface.LineBreak();
                    game = new Game(playerNum);
                    gameReturnVal = game.InitGame(args[1]);
                }
                else
                {
                    Console.WriteLine("Error: please run from the handler application.");
                    Console.ReadKey();
                    gameReturnVal = 1;
                }
            }
            return gameReturnVal;
        }
    }
}
