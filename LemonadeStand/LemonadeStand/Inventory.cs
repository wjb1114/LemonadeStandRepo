using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Inventory
    {
        public int currentMoney;
        public int currentLemons;
        public int currentSugar;
        public int currentCups;
        public int currentIce;

        public Inventory(int money, int lemons, int sugar, int cups, int ice)
        {
            currentMoney = money;
            currentLemons = lemons;
            currentSugar = sugar;
            currentCups = cups;
            currentIce = ice;
        }

        public void BuyIce(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= currentMoney)
            {
                currentIce += numBought;
                currentMoney -= (pricePer * numBought);
                data.moneySpent += (pricePer * numBought);
                Console.WriteLine("Bought " +  numBought + " Ice for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that much ice.");
            }
        }

        public void BuyLemons(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= currentMoney)
            {
                currentLemons += numBought;
                currentMoney -= (pricePer * numBought);
                data.moneySpent += (pricePer * numBought);
                Console.WriteLine("Bought " + numBought + " Lemons for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that many lemons.");
            }
        }

        public void BuyCups(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= currentMoney)
            {
                currentCups += numBought;
                currentMoney -= (pricePer * numBought);
                data.moneySpent += (pricePer * numBought);
                Console.WriteLine("Bought " + numBought + " Cups for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that many cups.");
            }
        }

        public void BuySugar(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= currentMoney)
            {
                currentSugar += numBought;
                currentMoney -= (pricePer * numBought);
                data.moneySpent += (pricePer * numBought);
                Console.WriteLine("Bought " + numBought + " Sugar for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that much sugar.");
            }
        }

        public void MeltIce()
        {
            if (currentIce > 0)
            {
                currentIce = 0;
                UserInterface.ChangeMode("All of your remaining ice melted.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}
