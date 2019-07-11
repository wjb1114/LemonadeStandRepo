using System;
using System.Collections.Generic;

namespace LemonadeStand
{
    class Game
    {
        
        int totalDays;
        int currentDayCount;
        Day currentDay;
        private readonly Inventory inv;
        private readonly Store store;

        public int PlayerNum { get; }

        public List<TrackedData> TrackedDataList { get; }

        public Game(int playerNumber)
        {
            inv = new Inventory(2000, 0, 0, 0, 0);
            store = new Store();
            TrackedDataList = new List<TrackedData>();
            totalDays = 0;
            currentDayCount = 1;
            PlayerNum = playerNumber;
        }

        public void StartGame()
        {
            bool notBankrupt = true;
            do
            {
                StartDay(notBankrupt);
                if (notBankrupt == true)
                {
                    notBankrupt = store.StoreMenu(inv, currentDay.Data);
                }
                if (notBankrupt == true)
                {
                    RunStand();
                }
                EndDay();
                UserInterface.ChangeMode("Waiting for other player(s).");
                while (System.IO.File.Exists("c:\\temp\\player" + PlayerNum + "day" + currentDayCount + ".bin"))
                {
                    SeededData.ClearKeyBuffer();
                    System.Threading.Thread.Sleep(500);
                }
                currentDayCount++;
            }
            while (currentDay.NumDay < totalDays);
            EndGame(notBankrupt);
        }

        public int InitGame(string numDaysStr)
        {
            int numDays = 0;
            
            try
            {
                numDays = Convert.ToInt32(numDaysStr);
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

        public void StartDay(bool notBankrupt)
        {
            currentDay = new Day(currentDayCount);
            if (notBankrupt == true)
            {
                UserInterface.ChangeMode("New Day");
                store.CalculateNewPrices();
                currentDay.Data.WeatherToday.DisplayForecast();
                Console.WriteLine("Press any key to shop for ingredients.");
            }
            else
            {
                UserInterface.ChangeMode("Bankrupt");
                Console.WriteLine("You are bankrupt!");
            }
            Console.ReadKey();
        }

        public void RunStand()
        {
            int inversePercent;

            // 8 hours, 60 minutes per hour
            // avg 100 customers on good day
            // approx 1 customer every 4 to 5 minutes during normal weather conditions
            currentDay.SetRecipe(inv);

            UserInterface.ChangeMode("Running Stand");

            currentDay.Data.WeatherToday.DisplayWeather();
            Console.WriteLine("Press any key to begin the day.");
            Console.ReadKey();

            if(currentDay.Data.WeatherToday.WeatherType == "sunny")
            {
                inversePercent = 74;
            }
            else if (currentDay.Data.WeatherToday.WeatherType == "partly cloudy")
            {
                inversePercent = 78;
            }
            else if (currentDay.Data.WeatherToday.WeatherType == "overcast")
            {
                inversePercent = 81;
            }
            else if (currentDay.Data.WeatherToday.WeatherType == "foggy")
            {
                inversePercent = 85;
            }
            else if (currentDay.Data.WeatherToday.WeatherType == "rainy")
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
            inv.UseLemons(currentDay.LemonsPerPitcher);
            inv.UseSugar(currentDay.SugarPerPitcher);
            

            for (int i = 0; i < 480; i++)
            {
                if (inv.CurrentCups < 1)
                {
                    Console.WriteLine("You are out of cups and close your stand for the day.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    break;
                }

                if (rand.Next(1, 101) > inversePercent)
                {
                    int qualityCount;
                    int minSour = rand.Next(1, 11);
                    int maxSour = rand.Next(minSour, 11);
                    int minSweet = rand.Next(1, 11);
                    int maxSweet = rand.Next(minSweet, 11);
                    int minWater = rand.Next(1, 11);
                    int maxWater = rand.Next(minWater, 11);
                    int maxPrice = rand.Next(25, 101);
                    cust = new Customer(maxSour, minSour, maxSweet, minSweet, maxWater, minWater, maxPrice);

                    cust.AdjustPreferencesForWeather(currentDay.Data.WeatherToday.Temperature);

                    qualityCount = cust.CheckDrinkCompatibility(currentDay.LemonsPerPitcher, currentDay.SugarPerPitcher, currentDay.IcePerCup);

                    if (currentDay.PricePerCup <= cust.maxPriceThreshold)
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
                                if (inv.CurrentIce < 1 || inv.CurrentIce < currentDay.IcePerCup)
                                {
                                    Console.WriteLine("You are out of ice and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                pitcherRemaining--;
                                inv.AddMoney(currentDay.PricePerCup);
                                inv.UseCups(1);
                                inv.UseIce(currentDay.IcePerCup);
                                if (pitcherRemaining < 1)
                                {
                                    if (inv.CurrentLemons < 1 || inv.CurrentLemons < currentDay.LemonsPerPitcher)
                                    {
                                        Console.WriteLine("You are out of lemons and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    if (inv.CurrentSugar < 1 || inv.CurrentSugar < currentDay.SugarPerPitcher)
                                    {
                                        Console.WriteLine("You are out of sugar and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    pitcherRemaining = 16;
                                    inv.UseSugar(currentDay.SugarPerPitcher);
                                    inv.UseLemons(currentDay.LemonsPerPitcher);
                                }
                                currentDay.Data.CustomerPurchased();
                                currentDay.Data.EarnMoney(currentDay.PricePerCup);
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
                                if (inv.CurrentIce < 1 || inv.CurrentIce < currentDay.IcePerCup)
                                {
                                    Console.WriteLine("You are out of ice and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                pitcherRemaining--;
                                inv.AddMoney(currentDay.PricePerCup);
                                inv.UseCups(1);
                                inv.UseIce(currentDay.IcePerCup);
                                if (pitcherRemaining < 1)
                                {
                                    if (inv.CurrentLemons < 1 || inv.CurrentLemons < currentDay.LemonsPerPitcher)
                                    {
                                        Console.WriteLine("You are out of lemons and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    if (inv.CurrentSugar < 1 || inv.CurrentSugar < currentDay.SugarPerPitcher)
                                    {
                                        Console.WriteLine("You are out of sugar and close your stand for the day.");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    pitcherRemaining = 16;
                                    inv.UseSugar(currentDay.SugarPerPitcher);
                                    inv.UseLemons(currentDay.LemonsPerPitcher);
                                }
                                currentDay.Data.CustomerPurchased();
                                currentDay.Data.EarnMoney(currentDay.PricePerCup);
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
                            if (inv.CurrentIce < 1 || inv.CurrentIce < currentDay.IcePerCup)
                            {
                                Console.WriteLine("You are out of ice and close your stand for the day.");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;
                            }
                            pitcherRemaining--;
                            inv.AddMoney(currentDay.PricePerCup);
                            inv.UseCups(1);
                            inv.UseIce(currentDay.IcePerCup);
                            if (pitcherRemaining < 1)
                            {
                                if (inv.CurrentLemons < 1 || inv.CurrentLemons < currentDay.LemonsPerPitcher)
                                {
                                    Console.WriteLine("You are out of lemons and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                if (inv.CurrentSugar < 1 || inv.CurrentSugar < currentDay.SugarPerPitcher)
                                {
                                    Console.WriteLine("You are out of sugar and close your stand for the day.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;
                                }
                                pitcherRemaining = 16;
                                inv.UseSugar(currentDay.SugarPerPitcher);
                                inv.UseLemons(currentDay.LemonsPerPitcher);
                            }
                            currentDay.Data.CustomerPurchased();
                            currentDay.Data.EarnMoney(currentDay.PricePerCup);
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
                    currentDay.Data.AddCustomerToList(cust);
                }
                else
                {
                    Console.WriteLine();
                }
                System.Threading.Thread.Sleep(41);
            }

            UserInterface.LineBreak();

            Console.WriteLine(currentDay.Data.CustomerList.Count + " customers appeared today.");
            Console.WriteLine(currentDay.Data.CustomersBought + " customers bought lemonade today.");
            Console.WriteLine("$" + currentDay.Data.MoneyEarned + " earned today.");
            Console.WriteLine("$" + currentDay.Data.MoneySpent + " spent today.");
            Console.WriteLine("Press any key to end the day.");
            Console.ReadKey();
        }

        public void EndDay()
        {
            UserInterface.ChangeMode("Cumulative Totals:");
            TrackedDataList.Add(currentDay.Data);

            SerializedData.SerializeDataDaily(currentDay.Data, PlayerNum, currentDay.NumDay);

            int totalCustomers = 0;
            int totalSales = 0;
            int totalMoneySpent = 0;
            int totalMoneyEarned = 0;

            for (int i = 0; i < TrackedDataList.Count; i++)
            {
                totalCustomers += TrackedDataList[i].CustomerList.Count;
                totalSales += TrackedDataList[i].CustomersBought;
                totalMoneySpent += TrackedDataList[i].MoneySpent;
                totalMoneyEarned += TrackedDataList[i].MoneyEarned;
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

            for (int i = 0; i < TrackedDataList.Count; i++)
            {
                totalCustomers += TrackedDataList[i].CustomerList.Count;
                totalSales += TrackedDataList[i].CustomersBought;
                totalMoneySpent += TrackedDataList[i].MoneySpent;
                totalMoneyEarned += TrackedDataList[i].MoneyEarned;
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
