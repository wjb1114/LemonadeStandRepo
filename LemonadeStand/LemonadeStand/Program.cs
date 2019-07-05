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
            Console.WriteLine("Welcome to \"Lemonade Stand\"!");
            Console.WriteLine("You start with $20 and need to get as much money as possible by running your own Lemonade Stand.");
            
            Game game = new Game();
            game.InitGame();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
