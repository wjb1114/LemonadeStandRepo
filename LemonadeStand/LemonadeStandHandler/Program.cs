using System;

namespace LemonadeStandHandler
{
    class Program
    {
        static void Main()
        {
            GameState gameState;
            bool goAgain;
            string goAgainStr;
            do
            {
                gameState = new GameState();
                gameState.RunGameState();

                do
                {
                    Console.WriteLine("Play again? Enter \"Yes\" or \"No\".");
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

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
