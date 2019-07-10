using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For SOLID principles as outlined in the user stories, please see the Weather.cs file for Open/Closed, and see Store.cs for Single Responsibility

namespace LemonadeStand
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error: please define player number.");
                Console.ReadKey();
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("Error: please run with only one argument.");
                Console.ReadKey();
            }
            else
            {
                Game game;

                Console.WriteLine("Welcome to \"Lemonade Stand\"!");
                Console.WriteLine("You start with $20 and need to get as much money as possible by running your own Lemonade Stand.");
                Console.WriteLine("----------");
                game = new Game();
                game.InitGame();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
