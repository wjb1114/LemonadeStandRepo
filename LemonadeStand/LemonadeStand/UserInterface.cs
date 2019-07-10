using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    static class UserInterface
    {
        public static void ChangeMode(string modeName)
        {
            Console.Clear();
            Console.WriteLine(modeName);
            LineBreak();
        }

        public static void LineBreak()
        {
            Console.WriteLine("\n----------\n");
        }
    }
}
