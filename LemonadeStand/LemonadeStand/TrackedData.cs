using System;
using System.Collections.Generic;

namespace LemonadeStand
{
    [Serializable]
    public class TrackedData
    {
        public int MoneySpent { get; private set; }
        public int MoneyEarned { get; private set; }
        public int CustomersBought { get; private set; }
        public List<Customer> CustomerList { get; }
        public Weather WeatherToday { get; }
        public TrackedData()
        {
            MoneyEarned = 0;
            MoneySpent = 0;
            CustomersBought = 0;
            CustomerList = new List<Customer>();
            WeatherToday = new Weather();
        }

        public void SpendMoney(int money)
        {
            MoneySpent += money;
        }

        public void EarnMoney(int money)
        {
            MoneyEarned += money;
        }

        public void CustomerPurchased()
        {
            CustomersBought += 1;
        }

        public void AddCustomerToList(Customer cust)
        {
            CustomerList.Add(cust);
        }
    }
}
