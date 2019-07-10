using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    [Serializable]
    class TrackedData
    {
        public int moneySpent;
        public int moneyEarned;
        public int customersBought;
        public List<Customer> customerList;
        public Weather weatherToday;
        public TrackedData()
        {
            moneyEarned = 0;
            moneySpent = 0;
            customersBought = 0;
            customerList = new List<Customer>();
            weatherToday = new Weather();
        }

        public void SpendMoney(int money)
        {
            moneySpent += money;
        }

        public void EarnMoney(int money)
        {
            moneyEarned += money;
        }

        public void CustomerPurchased()
        {
            customersBought += 1;
        }

        public void AddCustomerToList(Customer cust)
        {
            customerList.Add(cust);
        }
    }
}
