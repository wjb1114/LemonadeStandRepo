using System;

namespace LemonadeStand
{
    internal class Inventory
    {
        public int CurrentMoney { get; private set; }

        public int CurrentLemons { get; private set; }

        public int CurrentSugar { get; private set; }

        public int CurrentCups { get; private set; }

        public int CurrentIce { get; private set; }

        public Inventory(int money, int lemons, int sugar, int cups, int ice)
        {
            CurrentMoney = money;
            CurrentLemons = lemons;
            CurrentSugar = sugar;
            CurrentCups = cups;
            CurrentIce = ice;
        }

        public void BuyIce(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= CurrentMoney)
            {
                CurrentIce += numBought;
                CurrentMoney -= (pricePer * numBought);
                data.SpendMoney (pricePer * numBought);
                Console.WriteLine("Bought " +  numBought + " Ice for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that much ice.");
            }
        }

        public void BuyLemons(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= CurrentMoney)
            {
                CurrentLemons += numBought;
                CurrentMoney -= (pricePer * numBought);
                data.SpendMoney (pricePer * numBought);
                Console.WriteLine("Bought " + numBought + " Lemons for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that many lemons.");
            }
        }

        public void BuyCups(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= CurrentMoney)
            {
                CurrentCups += numBought;
                CurrentMoney -= (pricePer * numBought);
                data.SpendMoney (pricePer * numBought);
                Console.WriteLine("Bought " + numBought + " Cups for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that many cups.");
            }
        }

        public void BuySugar(int pricePer, int numBought, TrackedData data)
        {
            if ((pricePer * numBought) <= CurrentMoney)
            {
                CurrentSugar += numBought;
                CurrentMoney -= (pricePer * numBought);
                data.SpendMoney (pricePer * numBought);
                Console.WriteLine("Bought " + numBought + " Sugar for $" + (pricePer * numBought) + ".");
            }
            else
            {
                Console.WriteLine("You do not have enough money for that much sugar.");
            }
        }

        public void MeltIce()
        {
            if (CurrentIce > 0)
            {
                CurrentIce = 0;
                UserInterface.ChangeMode("All of your remaining ice melted.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        public void UseLemons(int numUsed)
        {
            CurrentLemons -= numUsed;
        }

        public void UseIce(int numUsed)
        {
            CurrentIce -= numUsed;
        }

        public void UseCups(int numUsed)
        {
            CurrentCups -= numUsed;
        }

        public void UseSugar(int numUsed)
        {
            CurrentSugar -= numUsed;
        }

        public void AddMoney(int numAdded)
        {
            CurrentMoney += numAdded;
        }
    }
}
