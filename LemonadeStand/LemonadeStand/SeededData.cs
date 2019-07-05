using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    static class SeededData
    {
        static string[] firstNames = new string[10] { "William", "Chris", "Michael", "Evan", "James", "Emily", "Amy", "Angela", "Nancy", "Chloe" };
        static string[] lastNames = new string[10] {"Stephens", "Jacobsen", "Adams", "Glaser", "Christoffsen", "Caulfield", "Miles", "Masterson", "DeAngelo", "Hauser"};

        public static string GenerateFullName()
        {
            string fullName = "";
            Random rand = new Random();
            fullName += firstNames[rand.Next(0, firstNames.Length)];
            System.Threading.Thread.Sleep(10);
            fullName += " ";
            fullName += lastNames[rand.Next(0, lastNames.Length)];
            System.Threading.Thread.Sleep(10);
            return fullName;
        }
    }
}
