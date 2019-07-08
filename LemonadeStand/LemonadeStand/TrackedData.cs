using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class TrackedData
    {
        int moneySpent;
        int moneyEarned;
        int customersAppeared;
        int customersBought;
        public TrackedData()
        {
            moneyEarned = 0;
            moneySpent = 0;
            customersAppeared = 0;
            customersBought = 0;
        }
    }
}
