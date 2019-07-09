using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Customer
    {
        string customerName;

        // number between 1 an 10 for each, corresponds directly with number of lemons/sugar/ice
        // ie 4 lemons, 3 sugar, 5 ice cubes = 4 sour, 3 sweet, 5 water

        public int maxSourThreshold;
        public int minSourThreshold;
        public int maxSweetThreshold;
        public int minSweetThreshold;
        public int maxWaterThreshold;
        public int minWaterThreshold;
        public int maxPriceThreshold;
        public bool didPurchase;
        public string feedbackStr;

        public Customer(int maxSour, int minSour, int maxSweet, int minSweet, int maxWater, int minWater, int maxPrice)
        {
            customerName = SeededData.GenerateFullName();
            maxSourThreshold = maxSour;
            minSourThreshold = minSour;
            maxSweetThreshold = maxSweet;
            minSweetThreshold = minSweet;
            maxWaterThreshold = maxWater;
            minWaterThreshold = minWater;
            maxPriceThreshold = maxPrice;
            didPurchase = false;
            feedbackStr = "";
        }

        public string GetCustomerName()
        {
            return customerName;
        }
    }
}
