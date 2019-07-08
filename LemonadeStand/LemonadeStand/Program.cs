using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game;
            bool goAgain = false;
            string goAgainStr = "";

            Console.WriteLine("Welcome to \"Lemonade Stand\"!");
            Console.WriteLine("You start with $20 and need to get as much money as possible by running your own Lemonade Stand.");            
            do
            {
                game = new Game();
                game.InitGame();
                do
                {
                    Console.WriteLine("Play again? \"Yes\" or \"No\".");
                    goAgainStr = Console.ReadLine();
                }
                while (goAgainStr.ToLower() != "yes" && goAgainStr.ToLower() != "no");
                if (goAgainStr.ToLower() == "yes")
                {
                    goAgain = true;
                }
                else
                {
                    goAgain = false;
                }
            }
            while (goAgain == true);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
