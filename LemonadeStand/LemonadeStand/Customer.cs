﻿using System;

namespace LemonadeStand
{
    [Serializable]
    public class Customer
    {
        private readonly string customerName;

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

        public int CheckDrinkCompatibility(int lemons, int sugar, int ice)
        {
            int totalCompatibility = 0;
            if (lemons > maxSourThreshold)
            {
                if (feedbackStr.Length > 0)
                {
                    feedbackStr += ", ";
                }
                feedbackStr += "Too sour";
            }
            else if (lemons < minSourThreshold)
            {
                if (feedbackStr.Length > 0)
                {
                    feedbackStr += ", ";
                }
                feedbackStr += "Not sour enough";
            }
            else
            {
                totalCompatibility++;
            }

            if (sugar > maxSweetThreshold)
            {
                if (feedbackStr.Length > 0)
                {
                    feedbackStr += ", ";
                }
                feedbackStr += "Too sweet";
            }
            else if (sugar < minSweetThreshold)
            {
                if (feedbackStr.Length > 0)
                {
                    feedbackStr += ", ";
                }
                feedbackStr += "Not sweet enough";
            }
            else
            {
                totalCompatibility++;
            }

            if (ice > maxWaterThreshold)
            {
                if (feedbackStr.Length > 0)
                {
                    feedbackStr += ", ";
                }
                feedbackStr += "Too much ice";
            }
            else if (ice < minWaterThreshold)
            {
                if (feedbackStr.Length > 0)
                {
                    feedbackStr += ", ";
                }
                feedbackStr += "Not enough ice";
            }
            else
            {
                totalCompatibility++;
            }

            return totalCompatibility;
        }

        public void AdjustPreferencesForWeather(int temperature)
        {
            if (temperature < 77)
            {
                minWaterThreshold--;
                maxWaterThreshold--;
                if (minWaterThreshold <= 0)
                {
                    minWaterThreshold++;
                }
                if (maxWaterThreshold <= 0)
                {
                    maxWaterThreshold++;
                }
            }
            else if (temperature > 89)
            {
                minWaterThreshold++;
                maxWaterThreshold++;
                if (minWaterThreshold >= 10)
                {
                    minWaterThreshold--;
                }
                if (maxWaterThreshold >= 10)
                {
                    maxWaterThreshold--;
                }
            }
        }
    }
}
