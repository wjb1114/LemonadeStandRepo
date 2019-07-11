using System;

namespace LemonadeStand
{
    class Day
    {
        public int NumDay { get; }
        public TrackedData Data { get; }
        public int LemonsPerPitcher { get; private set; }
        public int SugarPerPitcher { get; private set; }
        public int IcePerCup { get; private set; }
        public int PricePerCup { get; private set; }
        public Day(int dayCount)
        {
            NumDay = dayCount;
            Data = new TrackedData();
            LemonsPerPitcher = 0;
            SugarPerPitcher = 0;
            IcePerCup = 0;
            PricePerCup = 0;
        }

        public void SetRecipe(Inventory inv)
        {
            string inputStr;
            int inputInt = 0;
            bool errorThrown = false;
            bool validNumber = false;
            do
            {
                UserInterface.ChangeMode("How many Lemons per pitcher?");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = Convert.ToInt32(inputStr);
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
                if ((inputInt < 1 || inputInt > 10) && errorThrown == false)
                {
                    Console.WriteLine("Please enter a whole number greater than zero and less than 11.");
                }
                else if (errorThrown == true)
                {
                    errorThrown = false;
                }
                else if (inputInt > inv.CurrentLemons)
                {
                    Console.WriteLine("You don't have that much!");
                }
                else
                {
                    validNumber = true;
                }
            }
            while (validNumber == false);
            LemonsPerPitcher = inputInt;

            inputInt = 0;
            errorThrown = false;
            validNumber = false;


            do
            {
                UserInterface.ChangeMode("How much Sugar per pitcher?");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = Convert.ToInt32(inputStr);
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
                if ((inputInt < 1 || inputInt > 10) && errorThrown == false)
                {
                    Console.WriteLine("Please enter a whole number greater than zero and less than 11.");
                }
                else if (errorThrown == true)
                {
                    errorThrown = false;
                }
                else if (inputInt > inv.CurrentSugar)
                {
                    Console.WriteLine("You don't have that much!");
                }
                else
                {
                    validNumber = true;
                }
            }
            while (validNumber == false);
            SugarPerPitcher = inputInt;

            inputInt = 0;
            errorThrown = false;
            validNumber = false;

            do
            {
                UserInterface.ChangeMode("How much Ice per cup?");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = Convert.ToInt32(inputStr);
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
                if ((inputInt < 1 || inputInt > 10) && errorThrown == false)
                {
                    Console.WriteLine("Please enter a whole number greater than zero and less than 11.");
                }
                else if (errorThrown == true)
                {
                    errorThrown = false;
                }
                else if (inputInt > inv.CurrentIce)
                {
                    Console.WriteLine("You don't have that much!");
                }
                else
                {
                    validNumber = true;
                }
            }
            while (validNumber == false);
            IcePerCup = inputInt;

            inputInt = 0;
            errorThrown = false;
            validNumber = false;

            do
            {
                UserInterface.ChangeMode("How much will you charge per cup? Enter value in cents.");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = Convert.ToInt32(inputStr);
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
                if (inputInt < 1 && errorThrown == false)
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
                }
            }
            while (validNumber == false);
            PricePerCup = inputInt;
        }
    }
}
