using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SOLID Principle Open/Closed
// This class contains basic information for the weather.
// It's only methods are used for displaying the weather and displaying a forecast of the weather
// These methods don't have a need to be extended for further use, but the provided member variables can be used to create additional forecasts or weather reports at once, if needed in the future

namespace LemonadeStandHandler
{
    [Serializable]
    class Weather
    {
        public int temperature;
        int weatherTypeSelector;
        public string weatherType;

        public Weather()
        {
            Random rand = new Random();
            weatherTypeSelector = rand.Next(1, 11);
            temperature = rand.Next(65, 96);
            switch(weatherTypeSelector)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    weatherType = "sunny";
                    break;
                case 5:
                case 6:
                    weatherType = "partly cloudy";
                    break;
                case 7:
                case 8:
                    weatherType = "overcast";
                    break;
                case 9:
                    weatherType = "foggy";
                    break;
                case 10:
                    weatherType = "rainy";
                    break;
            }

        }

        public void DisplayForecast()
        {
            Random rand = new Random();
            int forecastTemp = temperature + rand.Next(-10, 11);
            int forecastSelector = weatherTypeSelector + rand.Next(-2, 3);
            if ((forecastSelector + 1) == weatherTypeSelector || (forecastSelector - 1) == weatherTypeSelector)
            {
                forecastSelector = weatherTypeSelector;
            }
            string forecastType = "";
            if (forecastSelector > 10)
            {
                forecastSelector = 10;
            }
            else if (forecastSelector < 1)
            {
                forecastSelector = 1;
            }

            switch (forecastSelector)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    forecastType = "sunny";
                    break;
                case 5:
                case 6:
                    forecastType = "partly cloudy";
                    break;
                case 7:
                case 8:
                    forecastType = "overcast";
                    break;
                case 9:
                    forecastType = "foggy";
                    break;
                case 10:
                    forecastType = "rainy";
                    break;
            }

            Console.WriteLine("Todays forecast:");
            Console.WriteLine("Around " + forecastTemp + " degrees and " + forecastType + ".");
        }

        public void DisplayWeather()
        {
            Console.WriteLine("It is currently " + temperature + " degrees and " + weatherType + ".");
        }
    }
}
