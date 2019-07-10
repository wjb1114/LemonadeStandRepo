using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Day
    {
        public int numDay;
        public TrackedData data;
        public int lemonsPerPitcher;
        public int sugarPerPitcher;
        public int icePerCup;
        public int pricePerCup;
        public Day(int dayCount)
        {
            numDay = dayCount;
            data = new TrackedData();
            lemonsPerPitcher = 0;
            sugarPerPitcher = 0;
            icePerCup = 0;
            pricePerCup = 0;
        }

        public void SetRecipe()
        {
            string inputStr = "";
            int inputInt = 0;
            bool errorThrown = false;
            bool validNumber = false;
            do
            {
                UserInterface.ChangeMode("How many Lemons per pitcher?");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = System.Convert.ToInt32(inputStr);
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
                else
                {
                    validNumber = true;
                }
            }
            while (validNumber == false);
            lemonsPerPitcher = inputInt;

            inputStr = "";
            inputInt = 0;
            errorThrown = false;
            validNumber = false;


            do
            {
                UserInterface.ChangeMode("How much Sugar per pitcher?");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = System.Convert.ToInt32(inputStr);
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
                else
                {
                    validNumber = true;
                }
            }
            while (validNumber == false);
            sugarPerPitcher = inputInt;

            inputStr = "";
            inputInt = 0;
            errorThrown = false;
            validNumber = false;

            do
            {
                UserInterface.ChangeMode("How much Ice per cup?");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = System.Convert.ToInt32(inputStr);
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
                else
                {
                    validNumber = true;
                }
            }
            while (validNumber == false);
            icePerCup = inputInt;

            inputStr = "";
            inputInt = 0;
            errorThrown = false;
            validNumber = false;

            do
            {
                UserInterface.ChangeMode("How much will you charge per cup? Enter value in cents.");
                inputStr = Console.ReadLine();
                try
                {
                    inputInt = System.Convert.ToInt32(inputStr);
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
            pricePerCup = inputInt;
        }
    }
}
