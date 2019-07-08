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
        List<TrackedData> trackedDataList;
        TrackedData data;

        public Game()
        {
            inv = new Inventory(2000, 0, 0, 0, 0);
            store = new Store();
            trackedDataList = new List<TrackedData>();
            totalDays = 0;
            currentDay = 1;
        }

        public void StartGame()
        {            
            Console.WriteLine("Started game with " + totalDays + " days runtime.");

            do
            {
                StartDay();
                store.StoreMenu(inv, data);
                RunStand();
                EndDay();
                currentDay++;
            }
            while (currentDay <= totalDays);
            EndGame();
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
        
        public void StartDay()
        {
            store.CalculateNewPrices();
            data = new TrackedData();
        }

        public void RunStand()
        {
            // 8 hours, 60 minutes per hour
            // avg 100 customers on good day
            // approx 1 customer every 4 to 5 minutes

            Random rand = new Random();
            Customer cust;

            int j = 0;

            for (int i = 0; i < 480; i++)
            {
                
                if (rand.Next(1, 101) > 79)
                {
                    j++;
                    cust = new Customer(rand.Next(6, 10), rand.Next(1, 6), rand.Next(6, 10), rand.Next(1, 6), rand.Next(6, 10), rand.Next(1, 6), rand.Next(25, 101));
                    Console.WriteLine(cust.GetCustomerName() + " is customer number " + j + ".");
                    data.SpawnedCustomer();
                    data.AddCustomerToList(cust);
                }
            }

            Console.WriteLine(data.customerList.Count + " customers appeared today.");
        }

        public void EndDay()
        {
            Console.WriteLine(data.moneySpent);
            trackedDataList.Add(data);
        }

        public void EndGame()
        {

        }
    }
}
