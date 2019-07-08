using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Game
    {
        
        int totalDays;
        int currentDay;
        Inventory inv;
        Store store;

        public Game()
        {
            inv = new Inventory(2000, 0, 0, 0, 0);
            store = new Store();
            totalDays = 0;
            currentDay = 1;
        }

        public void StartGame()
        {            
            Console.WriteLine("Started game with " + totalDays + " days runtime.");

            do
            {
                store.StoreMenu(inv);
                RunStand();
                EndDay();
                currentDay++;
            }
            while (currentDay <= totalDays);
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

        

        public void RunStand()
        {

        }

        public void EndDay()
        {

        }
    }
}
