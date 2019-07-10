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
        static int Main(string[] args)
        {
            int gameReturnVal;
            if (args.Length < 2)
            {
                Console.WriteLine("Error: please define player number and number of days to run.");
                Console.ReadKey();
                gameReturnVal = 1;
            }
            else if (args.Length > 2)
            {
                Console.WriteLine("Error: please run with only two arguments.");
                Console.ReadKey();
                gameReturnVal = 1;
            }
            else
            {
                int playerNum = System.Convert.ToInt32(args[0]);
                Game game;

                Console.WriteLine("Welcome to \"Lemonade Stand\"!");
                Console.WriteLine("You start with $20 and need to get as much money as possible by running your own Lemonade Stand.");
                UserInterface.LineBreak();
                game = new Game(playerNum);
                gameReturnVal = game.InitGame(args[1]);
                if (gameReturnVal == 0)
                {
                    SerializedData.SerializeDataEnd(game.trackedDataList, playerNum);
                }
            }
            return gameReturnVal;
        }
    }
}
