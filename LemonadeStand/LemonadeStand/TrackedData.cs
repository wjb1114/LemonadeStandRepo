using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class TrackedData
    {
        public int moneySpent;
        public int moneyEarned;
        int customersAppeared;
        int customersBought;
        public TrackedData()
        {
            moneyEarned = 0;
            moneySpent = 0;
            customersAppeared = 0;
            customersBought = 0;
        }

        public void SpendMoney(int money)
        {
            moneySpent += money;
        }

        public void EarnMoney(int money)
        {
            moneyEarned += money;
        }

        public void SpawnedCustomer()
        {
            customersAppeared += 1;
        }

        public void CustomerPurchased()
        {
            customersBought += 1;
        }
    }
}
