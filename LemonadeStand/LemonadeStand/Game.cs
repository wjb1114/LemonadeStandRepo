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
        int currentDayCount;
        Day currentDay;
        Inventory inv;
        Store store;
        public List<TrackedData> trackedDataList;

        public Game()
        {
            inv = new Inventory(2000, 0, 0, 0, 0);
            store = new Store();
            trackedDataList = new List<TrackedData>();
            totalDays = 0;
            currentDayCount = 1;
        }

        public void StartGame()
        {
            bool notBankrupt;
            do
            {
                StartDay();
                notBankrupt = store.StoreMenu(inv, currentDay.data);
                if (notBankrupt == false)
                {
                    trackedDataList.Add(currentDay.data);
                    break;
                }
                RunStand();
                EndDay();
                currentDayCount++;
            }
            while (currentDay.numDay <= totalDays);
            EndGame(notBankrupt);
        }

        public int InitGame(string numDaysStr)
        {
            int numDays = 0;
            
            try
            {
                numDays = System.Convert.ToInt32(numDaysStr);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: invalid length. Please ensure you are passing a number in the command line arguments.");
                return 1;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: interger overflow. Please pass a smaller whole number.");
                return 1;
            }
            if (numDays < 7)
            {
                Console.WriteLine("Error: Please pass a runtime of 7 days or greater.");
                return 1;
            }
            totalDays = numDays;

            StartGame();
            return 0;
        }
        
        public void StartDay()
        {
            UserInterface.ChangeMode("New Day");
            currentDay = new Day(currentDayCount);
            store.CalculateNewPrices();
            currentDay.data.weatherToday.DisplayForecast();
            Console.WriteLine("Press any key to shop for ingredients.");
            Console.ReadKey();
        }

        public void RunStand()
        {
            int inversePercent;

            // 8 hours, 60 minutes per hour
            // avg 100 customers on good day
            // approx 1 customer every 4 to 5 minutes during normal weather conditions
            currentDay.SetRecipe();

            UserInterface.ChangeMode("Running Stand");

            currentDay.data.weatherToday.DisplayWeather();
            Console.WriteLine("Press any key to begin the day.");
            Console.ReadKey();

            if(currentDay.data.weatherToday.weatherType == "sunny")
            {
                inversePercent = 74;
            }
            else if (currentDay.data.weatherToday.weatherType == "partly cloudy")
            {
                inversePercent = 78;
            }
            else if (currentDay.data.weatherToday.weatherType == "overcast")
            {
                inversePercent = 81;
            }
            else if (currentDay.data.weatherToday.weatherType == "foggy")
            {
                inversePercent = 85;
            }
            else if (currentDay.data.weatherToday.weatherType == "rainy")
            {
                inversePercent = 89;
            }
            else
            {
                inversePercent = 79;
            }

            Random rand = new Random();
            Customer cust;

            int pitcherRemaining = 16;
            inv.currentLemons -= currentDay.lemonsPerPitcher;
            inv.currentSugar -= currentDay.sugarPerPitcher;
            

            for (int i = 0; i < 480; i++)
            {
                if (inv.currentCups < 1)
                {
                    Console.WriteLine("You are out of cups and close your stand for the day.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    break;
                }

                if (rand.Next(1, 101) > inversePercent)
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

                    cust.AdjustPreferencesForWeather(currentDay.data.weatherToday.temperature);

                    qualityCount = cust.CheckDrinkCompatibility(currentDay.lemonsPerPitcher, currentDay.sugarPerPitcher, currentDay.icePerCup);

                    if (currentDay.pricePerCup <= cust.maxPriceThreshold)
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
                                if (inv.currentIce < 1 || inv.currentIce < currentDay.icePerCup)
                                {
                                    Console.WriteLine("You are out of ice and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                pitcherRemaining--;
                                inv.currentMoney += currentDay.pricePerCup;
                                inv.currentCups--;
                                inv.currentIce -= currentDay.icePerCup;
                                if (pitcherRemaining < 1)
                                {
                                    if (inv.currentLemons < 1 || inv.currentLemons < currentDay.lemonsPerPitcher)
                                    {
                                        Console.WriteLine("You are out of lemons and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    if (inv.currentSugar < 1 || inv.currentSugar < currentDay.sugarPerPitcher)
                                    {
                                        Console.WriteLine("You are out of sugar and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    pitcherRemaining = 16;
                                    inv.currentSugar -= currentDay.sugarPerPitcher;
                                    inv.currentLemons -= currentDay.lemonsPerPitcher;
                                }
                                currentDay.data.CustomerPurchased();
                                currentDay.data.EarnMoney(currentDay.pricePerCup);
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
                                if (inv.currentIce < 1 || inv.currentIce < currentDay.icePerCup)
                                {
                                    Console.WriteLine("You are out of ice and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                pitcherRemaining--;
                                inv.currentMoney += currentDay.pricePerCup;
                                inv.currentCups--;
                                inv.currentIce -= currentDay.icePerCup;
                                if (pitcherRemaining < 1)
                                {
                                    if (inv.currentLemons < 1 || inv.currentLemons < currentDay.lemonsPerPitcher)
                                    {
                                        Console.WriteLine("You are out of lemons and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    if (inv.currentSugar < 1 || inv.currentSugar < currentDay.sugarPerPitcher)
                                    {
                                        Console.WriteLine("You are out of sugar and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    pitcherRemaining = 16;
                                    inv.currentSugar -= currentDay.sugarPerPitcher;
                                    inv.currentLemons -= currentDay.lemonsPerPitcher;
                                }
                                currentDay.data.CustomerPurchased();
                                currentDay.data.EarnMoney(currentDay.pricePerCup);
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
                            if (inv.currentIce < 1 || inv.currentIce < currentDay.icePerCup)
                            {
                                Console.WriteLine("You are out of ice and close your stand for the day.");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;
                            }
                            pitcherRemaining--;
                            inv.currentMoney += currentDay.pricePerCup;
                            inv.currentCups--;
                            inv.currentIce -= currentDay.icePerCup;
                            if (pitcherRemaining < 1)
                            {
                                if (inv.currentLemons < 1 || inv.currentLemons < currentDay.lemonsPerPitcher)
                                {
                                    Console.WriteLine("You are out of lemons and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                if (inv.currentSugar < 1 || inv.currentSugar < currentDay.sugarPerPitcher)
                                {
                                    Console.WriteLine("You are out of sugar and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                pitcherRemaining = 16;
                                inv.currentSugar -= currentDay.sugarPerPitcher;
                                inv.currentLemons -= currentDay.lemonsPerPitcher;
                            }
                            currentDay.data.CustomerPurchased();
                            currentDay.data.EarnMoney(currentDay.pricePerCup);
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
                        Console.WriteLine(cust.GetCustomerName() + " did not purchase. " + cust.feedbackStr);
                    }
                    currentDay.data.AddCustomerToList(cust);
                }
                else
                {
                    Console.WriteLine();
                }
                System.Threading.Thread.Sleep(41);
            }

            UserInterface.LineBreak();

            Console.WriteLine(currentDay.data.customerList.Count + " customers appeared today.");
            Console.WriteLine(currentDay.data.customersBought + " customers bought lemonade today.");
            Console.WriteLine("$" + currentDay.data.moneyEarned + " earned today.");
            Console.WriteLine("$" + currentDay.data.moneySpent + " spent today.");
            Console.WriteLine("Press any key to end the day.");
            Console.ReadKey();
        }

        public void EndDay()
        {
            UserInterface.ChangeMode("Cumulative Totals:");
            trackedDataList.Add(currentDay.data);

            int totalCustomers = 0;
            int totalSales = 0;
            int totalMoneySpent = 0;
            int totalMoneyEarned = 0;

            for (int i = 0; i < trackedDataList.Count; i++)
            {
                totalCustomers += trackedDataList[i].customerList.Count;
                totalSales += trackedDataList[i].customersBought;
                totalMoneySpent += trackedDataList[i].moneySpent;
                totalMoneyEarned += trackedDataList[i].moneyEarned;
            }

            Console.WriteLine("Total of " + totalCustomers + " customers appeared.");
            Console.WriteLine("Total of " + totalSales + " customers bought lemonade.");
            Console.WriteLine("Total of $" + totalMoneyEarned + " earned.");
            Console.WriteLine("Total of $" + totalMoneySpent + " spent.");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            inv.MeltIce();
        }

        public void EndGame(bool isNotBankrupt)
        {
            UserInterface.ChangeMode("End of Game Totals:");
            int totalCustomers = 0;
            int totalSales = 0;
            int totalMoneySpent = 0;
            int totalMoneyEarned = 0;

            for (int i = 0; i < trackedDataList.Count; i++)
            {
                totalCustomers += trackedDataList[i].customerList.Count;
                totalSales += trackedDataList[i].customersBought;
                totalMoneySpent += trackedDataList[i].moneySpent;
                totalMoneyEarned += trackedDataList[i].moneyEarned;
            }

            Console.WriteLine("Total of " + totalCustomers + " customers appeared.");
            Console.WriteLine("Total of " + totalSales + " customers bought lemonade.");
            Console.WriteLine("Total of $" + totalMoneyEarned + " earned.");
            Console.WriteLine("Total of $" + totalMoneySpent + " spent.");
            if (isNotBankrupt == true)
            {
                Console.WriteLine("You did not go bankrupt.");
            }
            else
            {
                Console.WriteLine("You went bankrupt and were forced to close down your stand.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        
    }
}
