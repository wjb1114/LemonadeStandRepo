using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Game
    {
        int currentMoney;
        int currentLemons;
        int currentSugar;
        int currentCups;
        int currentIce;
        int totalDays;
        int currentDay;

        public Game()
        {
            currentMoney = 2000;
            currentLemons = 0;
            currentSugar = 0;
            currentCups = 0;
            currentIce = 0;
            totalDays = 0;
            currentDay = 1;
        }

        public void StartGame()
        {            
            Console.WriteLine("Started game with " + totalDays + " days runtime.");
        }

        public void InitGame()
        {
            bool validNumber = false;
            bool errorThrown = false;
            string numDaysStr = "";
            int numDays = 0;
            do
            {
                Console.WriteLine("How many days will you run your stand? If you are new to the game, it is recommended to start with 7 days.");
                numDaysStr = Console.ReadLine();
                try
                {
                    numDays = System.Convert.ToInt32(numDaysStr);
                }
                catch (FormatException)
                {

                    Console.WriteLine("Please enter a valid whole number.");
                    errorThrown = true;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Please enter a smaller whole number.");
                    errorThrown = true;
                }
                if (numDays < 1 && errorThrown == false)
                {
                    Console.WriteLine("Please enter a whole number greater than zero.");
                }
                else if (errorThrown == true)
                {
                    errorThrown = false;
                }
                else
                {
                    validNumber = true;
                    Console.WriteLine("You will run your stand for " + numDays + " days.");
                    Console.ReadKey();
                }
            }
            while (validNumber == false);
            totalDays = numDays;

            StartGame();
        }
    }
}
