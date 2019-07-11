using System;

// SOLID Principle Single Responsibility
// This class contains member variables for prices for each ingredient
// The methods present in this class are exclusively used for modifying the above pricing and using the pricing to facilitate purchase of ingredients

namespace LemonadeStand
{
    class Store
    {
        int pricePerLemon;
        int pricePerIce;
        int pricePerSugar;
        int pricePerCup;
        private readonly Random rand;
        public Store()
        {
            pricePerCup = 3;
            pricePerIce = 1;
            pricePerSugar = 9;
            pricePerLemon = 8;
            rand = new Random();
        }

        public int GiveLemonPrice()
        {
            return pricePerLemon;
        }

        public int GiveIcePrice()
        {
            return pricePerIce;
        }
        public int GiveCupPrice()
        {
            return pricePerCup;
        }

        public int GiveSugarPrice()
        {
            return pricePerSugar;
        }

        public void CalculateNewPrices()
        {
            pricePerCup += rand.Next(-2, 3);
            pricePerIce += rand.Next(-1, 2);
            pricePerSugar += rand.Next(-5, 6);
            pricePerLemon += rand.Next(-4, 5);

            if (pricePerCup <= 0)
            {
                pricePerCup = 1;
            }
            else if (pricePerCup > 50)
            {
                pricePerCup = 10;
            }

            if (pricePerIce <= 0)
            {
                pricePerIce = 1;
            }
            else if (pricePerIce > 15)
            {
                pricePerIce = 5;
            }

            if (pricePerSugar <= 0)
            {
                pricePerSugar = 1;
            }
            else if (pricePerSugar > 100)
            {
                pricePerSugar = 20;
            }

            if (pricePerLemon <= 0)
            {
                pricePerLemon = 1;
            }
            else if (pricePerCup > 100)
            {
                pricePerCup = 20;
            }

        }
        public bool StoreMenu(Inventory inv, TrackedData data)
        {
            UserInterface.ChangeMode("Store");
            string userInput;
            string numPurchaseStr;
            int numPurchase = 0;
            bool validNum;

            do
            {
                if ((inv.CurrentMoney < pricePerIce && inv.CurrentIce < 1) || (inv.CurrentMoney < pricePerSugar && inv.CurrentSugar < 1) || (inv.CurrentMoney < pricePerLemon && inv.CurrentLemons < 1) || (inv.CurrentMoney < pricePerCup && inv.CurrentCups < 1))
                {
                    Console.WriteLine("You do not have enough money or inventory to continue. Game over.");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.WriteLine("You have $" + inv.CurrentMoney + ", " + inv.CurrentCups + " cups, " + inv.CurrentIce + " ice, " + inv.CurrentLemons + " lemons, and " + inv.CurrentSugar + " sugar.");
                    Console.WriteLine("Cups are $" + pricePerCup + ", Ice is $" + pricePerIce + ", Lemons are $" + pricePerLemon + ", and Sugar is $" + pricePerSugar + ".");
                    Console.WriteLine("What will you purchase? Enter \"lemons\", \"ice\", \"sugar\", or \"cups\". Enter \"exit\" to run your stand.");
                    userInput = Console.ReadLine();
                    UserInterface.ChangeMode("Store");

                    if (userInput.ToLower() == "exit")
                    {
                        if (inv.CurrentLemons < 1 || inv.CurrentIce < 1 || inv.CurrentSugar < 1 || inv.CurrentCups < 1)
                        {
                            Console.WriteLine("Please make sure you have at least one of each item in your inventory.");
                        }
                    }
                    else if (userInput.ToLower() == "lemons" || userInput.ToLower() == "ice" || userInput.ToLower() == "sugar" || userInput.ToLower() == "cups")
                    {
                        do
                        {
                            bool errorThrown = false;
                            validNum = false;
                            Console.WriteLine("How much will you buy?");
                            numPurchaseStr = Console.ReadLine();
                            UserInterface.ChangeMode("Store");
                            try
                            {
                                numPurchase = Convert.ToInt32(numPurchaseStr);
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
                            if (numPurchase < 1 && errorThrown == false)
                            {
                                Console.WriteLine("Please enter a whole number greater than zero.");
                            }
                            else
                            {
                                validNum = true;
                            }
                        }
                        while (validNum == false);

                        if (userInput.ToLower() == "lemons")
                        {
                            inv.BuyLemons(GiveLemonPrice(), numPurchase, data);
                        }
                        else if (userInput.ToLower() == "ice")
                        {
                            inv.BuyIce(GiveIcePrice(), numPurchase, data);
                        }
                        else if (userInput.ToLower() == "sugar")
                        {
                            inv.BuySugar(GiveSugarPrice(), numPurchase, data);
                        }
                        else if (userInput.ToLower() == "cups")
                        {
                            inv.BuyCups(GiveCupPrice(), numPurchase, data);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unrecognized input.");
                    }
                }
            }
            while (userInput.ToLower() != "exit" || inv.CurrentLemons < 1 || inv.CurrentIce < 1 || inv.CurrentSugar < 1 || inv.CurrentCups < 1);
            return true;
        }

    }
}
