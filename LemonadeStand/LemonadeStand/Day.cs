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
    }
}
