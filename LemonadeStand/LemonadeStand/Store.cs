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
            pricePerCup = 10;
            pricePerIce = 1;
            pricePerSugar = 12;
            pricePerLemon = 15;
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
            pricePerCup += rand.Next(-1, 2);
            pricePerIce += rand.Next(-1, 2);
            pricePerSugar += rand.Next(-1, 2);
            pricePerLemon += rand.Next(-1, 2);
        }
        public void StoreMenu(Inventory inv, TrackedData data)
        {
            string userInput = "";
            string numPurchaseStr = "";
            int numPurchase = 0;
            bool validNum = false;

            do
            {
                Console.WriteLine("What will you purchase? Enter \"lemons\", \"ice\", \"sugar\", or \"cups\". Enter \"exit\" to run your stand.");
                userInput = Console.ReadLine();

                if (userInput.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting store. Press any key to start your stand for the day.");
                    Console.ReadKey();
                }
                else if (userInput.ToLower() == "lemons" || userInput.ToLower() == "ice" || userInput.ToLower() == "sugar" || userInput.ToLower() == "cups")
                {
                    do
                    {
                        bool errorThrown = false;
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
            while (userInput.ToLower() != "exit");

        }

    }
}
