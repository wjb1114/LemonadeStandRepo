using System;

// SOLID Principle Open/Closed
// This class contains basic information for the weather.
// It's only methods are used for displaying the weather and displaying a forecast of the weather
// These methods don't have a need to be extended for further use, but the provided member variables can be used to create additional forecasts or weather reports at once, if needed in the future

namespace LemonadeStand
{
    [Serializable]
    public class Weather
    {
        readonly int weatherTypeSelector;

        public int Temperature { get; }

        public string WeatherType { get; }

        public Weather()
        {
            Random rand = new Random();
            weatherTypeSelector = rand.Next(1, 11);
            Temperature = rand.Next(65, 96);
            switch(weatherTypeSelector)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    WeatherType = "sunny";
                    break;
                case 5:
                case 6:
                    WeatherType = "partly cloudy";
                    break;
                case 7:
                case 8:
                    WeatherType = "overcast";
                    break;
                case 9:
                    WeatherType = "foggy";
                    break;
                case 10:
                    WeatherType = "rainy";
                    break;
            }

        }

        public void DisplayForecast()
        {
            Random rand = new Random();
            int forecastTemp = Temperature + rand.Next(-10, 11);
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
            Console.WriteLine("It is currently " + Temperature + " degrees and " + WeatherType + ".");
        }
    }
}
