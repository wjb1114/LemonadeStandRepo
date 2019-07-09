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
        int lemonsPerPitcher;
        int sugarPerPitcher;
        int icePerCup;
        int pricePerCup;

        public Game()
        {
            inv = new Inventory(2000, 0, 0, 0, 0);
            store = new Store();
            trackedDataList = new List<TrackedData>();
            totalDays = 0;
            currentDay = 1;
            lemonsPerPitcher = 0;
            sugarPerPitcher = 0;
            icePerCup = 0;
            pricePerCup = 0;
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
            SetRecipe();

            Random rand = new Random();
            Customer cust;

            int pitcherRemaining = 16;
            inv.currentLemons -= lemonsPerPitcher;
            inv.currentSugar -= sugarPerPitcher;
            

            for (int i = 0; i < 480; i++)
            {
                if (inv.currentCups < 1)
                {
                    Console.WriteLine("You are out of cups and close your stand for the day.");
                    break;
                }

                if (rand.Next(1, 101) > 79)
                {
                    int qualityCount = 0;
                    int minSour = rand.Next(1, 11);
                    int maxSour = rand.Next(minSour, 11);
                    int minSweet = rand.Next(1, 11);
                    int maxSweet = rand.Next(minSweet, 11);
                    int minWater = rand.Next(1, 11);
                    int maxWater = rand.Next(minWater, 11);
                    int maxPrice = rand.Next(25, 101);
                    cust = new Customer(maxSour, minSour, maxSweet, minSweet, maxWater, minWater, maxPrice);

                    if (lemonsPerPitcher > cust.maxSourThreshold)
                    {
                        if (cust.feedbackStr.Length > 0)
                        {
                            cust.feedbackStr += ", ";
                        }
                        cust.feedbackStr += "Too Sour";
                    }
                    else if (lemonsPerPitcher < cust.minSourThreshold)
                    {
                        if (cust.feedbackStr.Length > 0)
                        {
                            cust.feedbackStr += ", ";
                        }
                        cust.feedbackStr += "Not Sour Enough";
                    }
                    else
                    {
                        qualityCount++;
                    }

                    if (sugarPerPitcher > cust.maxSweetThreshold)
                    {
                        if (cust.feedbackStr.Length > 0)
                        {
                            cust.feedbackStr += ", ";
                        }
                        cust.feedbackStr += "Too Sweet";
                    }
                    else if (sugarPerPitcher < cust.minSweetThreshold)
                    {
                        if (cust.feedbackStr.Length > 0)
                        {
                            cust.feedbackStr += ", ";
                        }
                        cust.feedbackStr += "Not Sweet Enough";
                    }
                    else
                    {
                        qualityCount++;
                    }

                    if (icePerCup > cust.maxWaterThreshold)
                    {
                        if (cust.feedbackStr.Length > 0)
                        {
                            cust.feedbackStr += ", ";
                        }
                        cust.feedbackStr += "Too Much Ice";
                    }
                    else if (icePerCup < cust.minWaterThreshold)
                    {
                        if (cust.feedbackStr.Length > 0)
                        {
                            cust.feedbackStr += ", ";
                        }
                        cust.feedbackStr += "Not Enough Ice";
                    }
                    else
                    {
                        qualityCount++;
                    }

                    if (pricePerCup <= cust.maxPriceThreshold)
                    {
                        if (qualityCount < 0)
                        {
                            Console.WriteLine("Something went wrong...");
                            Console.ReadKey();
                        }
                        else if (qualityCount == 0)
                        {
                            Console.WriteLine(cust.GetCustomerName() + " did not purchase. " + cust.feedbackStr);
                        }
                        else if (qualityCount == 1)
                        {
                            if(rand.Next(1, 5) > 3)
                            {
                                Console.WriteLine(cust.GetCustomerName() + " purchased. " + cust.feedbackStr);
                                if (inv.currentIce < 1 || inv.currentIce < icePerCup)
                                {
                                    Console.WriteLine("You are out of ice and close your stand for the day.");
                                    break;
                                }
                                pitcherRemaining--;
                                inv.currentMoney += pricePerCup;
                                inv.currentCups--;
                                inv.currentIce -= icePerCup;
                                if (pitcherRemaining < 1)
                                {
                                    if (inv.currentLemons < 1 || inv.currentLemons < lemonsPerPitcher)
                                    {
                                        Console.WriteLine("You are out of lemons and close your stand for the day.");
                                        break;
                                    }
                                    if (inv.currentSugar < 1 || inv.currentSugar < sugarPerPitcher)
                                    {
                                        Console.WriteLine("You are out of sugar and close your stand for the day.");
                                        break;
                                    }
                                    pitcherRemaining = 16;
                                    inv.currentSugar -= sugarPerPitcher;
                                    inv.currentLemons -= lemonsPerPitcher;
                                }
                                data.CustomerPurchased();
                                data.EarnMoney(pricePerCup);
                            }
                            else
                            {
                                Console.WriteLine(cust.GetCustomerName() + " did not purchase. " + cust.feedbackStr);
                            }
                        }
                        else if (qualityCount == 2)
                        {
                            if (rand.Next(1, 5) > 1)
                            {
                                Console.WriteLine(cust.GetCustomerName() + " purchased. " + cust.feedbackStr);
                                if (inv.currentIce < 1 || inv.currentIce < icePerCup)
                                {
                                    Console.WriteLine("You are out of ice and close your stand for the day.");
                                    break;
                                }
                                pitcherRemaining--;
                                inv.currentMoney += pricePerCup;
                                inv.currentCups--;
                                inv.currentIce -= icePerCup;
                                if (pitcherRemaining < 1)
                                {
                                    if (inv.currentLemons < 1 || inv.currentLemons < lemonsPerPitcher)
                                    {
                                        Console.WriteLine("You are out of lemons and close your stand for the day.");
                                        break;
                                    }
                                    if (inv.currentSugar < 1 || inv.currentSugar < sugarPerPitcher)
                                    {
                                        Console.WriteLine("You are out of sugar and close your stand for the day.");
                                        break;
                                    }
                                    pitcherRemaining = 16;
                                    inv.currentSugar -= sugarPerPitcher;
                                    inv.currentLemons -= lemonsPerPitcher;
                                }
                                data.CustomerPurchased();
                                data.EarnMoney(pricePerCup);
                            }
                            else
                            {
                                Console.WriteLine(cust.GetCustomerName() + " did not purchase. " + cust.feedbackStr);
                            }
                        }
                        else if (qualityCount == 3)
                        {
                            cust.feedbackStr += "Perfect!";
                            Console.WriteLine(cust.GetCustomerName() + " purchased. " + cust.feedbackStr);
                            if (inv.currentIce < 1 || inv.currentIce < icePerCup)
                            {
                                Console.WriteLine("You are out of ice and close your stand for the day.");
                                break;
                            }
                            pitcherRemaining--;
                            inv.currentMoney += pricePerCup;
                            inv.currentCups--;
                            inv.currentIce -= icePerCup;
                            if (pitcherRemaining < 1)
                            {
                                if (inv.currentLemons < 1 || inv.currentLemons < lemonsPerPitcher)
                                {
                                    Console.WriteLine("You are out of lemons and close your stand for the day.");
                                    break;
                                }
                                if (inv.currentSugar < 1 || inv.currentSugar < sugarPerPitcher)
                                {
                                    Console.WriteLine("You are out of sugar and close your stand for the day.");
                                    break;
                                }
                                pitcherRemaining = 16;
                                inv.currentSugar -= sugarPerPitcher;
                                inv.currentLemons -= lemonsPerPitcher;
                            }
                            data.CustomerPurchased();
                            data.EarnMoney(pricePerCup);
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        if (cust.feedbackStr.Length > 0)
                        {
                            cust.feedbackStr += ", ";
                        }
                        cust.feedbackStr += "Too Expensive";
                    }
                    data.AddCustomerToList(cust);
                }
                else
                {
                    Console.WriteLine();
                }
                System.Threading.Thread.Sleep(41);
            }

            Console.WriteLine(data.customerList.Count + " customers appeared today.");
            Console.WriteLine(data.customersBought + " customers bought lemonade.");
            Console.WriteLine("$" + data.moneyEarned + " earned today.");
            Console.WriteLine("$" + data.moneySpent + " spent today.");
        }

        public void EndDay()
        {
            trackedDataList.Add(data);
            lemonsPerPitcher = 0;
            sugarPerPitcher = 0;
            icePerCup = 0;
            pricePerCup = 0;
            inv.MeltIce();
        }

        public void EndGame()
        {

        }

        public void SetRecipe()
        {
            Console.WriteLine("How many Lemons per Pitcher?");
            lemonsPerPitcher = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How much Sugar per Pitcher?");
            sugarPerPitcher = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How much Ice per Cup?");
            icePerCup = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How much will you charge per cup? Value is in cents.");
            pricePerCup = Convert.ToInt32(Console.ReadLine());
        }
    }
}
