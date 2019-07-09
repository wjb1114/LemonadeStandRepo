using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Store
    {
        int pricePerLemon;
        int pricePerIce;
        int pricePerSugar;
        int pricePerCup;
        Random rand;
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
        public void StoreMenu(Inventory inv, TrackedData data)
        {
            string userInput = "";
            string numPurchaseStr = "";
            int numPurchase = 0;
            bool validNum = false;

            do
            {
                Console.WriteLine("You have $" + inv.currentMoney + ", " + inv.currentCups + " cups, " + inv.currentIce + " ice, " + inv.currentLemons + " lemons, and " + inv.currentSugar + " sugar.");
                Console.WriteLine("Cups are $" + pricePerCup + ", Ice is $" + pricePerIce + ", Lemons are $" + pricePerLemon + ", and Sugar is $" + pricePerSugar  + ".");
                Console.WriteLine("What will you purchase? Enter \"lemons\", \"ice\", \"sugar\", or \"cups\". Enter \"exit\" to run your stand.");
                userInput = Console.ReadLine();

                if (userInput.ToLower() == "exit")
                {
                    if (inv.currentLemons < 1 || inv.currentIce < 1 || inv.currentSugar < 1 || inv.currentCups < 1)
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
                        try
                        {
                            numPurchase = System.Convert.ToInt32(numPurchaseStr);
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
                        else if (errorThrown == true)
                        {
                            errorThrown = false;
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
            while (userInput.ToLower() != "exit" || inv.currentLemons < 1 || inv.currentIce < 1 || inv.currentSugar < 1 || inv.currentCups < 1);

        }

    }
}
