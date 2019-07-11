using System;

namespace LemonadeStand
{
    [Serializable]
    public static class SeededData
    {
        static readonly string[] firstNames = new string[12] { "William", "Chris", "Michael", "Evan", "James", "Emily", "Amy", "Angela", "Nancy", "Chloe", "Alice", "Adam" };
        static readonly string[] lastNames = new string[11] {"Stephens", "Jacobsen", "Adams", "Glaser", "Christoffsen", "Caulfield", "Miles", "Masterson", "DeAngelo", "Hauser", "Murphy"};
        public static string key = "testKey852";

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

        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(false);
        }
    }
}
