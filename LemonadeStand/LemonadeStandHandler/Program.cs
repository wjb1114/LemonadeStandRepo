using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using LemonadeStand;

namespace LemonadeStandHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<TrackedData>> dataList = new List<List<TrackedData>>();
            List<int> playerScores = new List<int>();
            Dictionary<string, Process> processes = new Dictionary<string, Process>(); ;
            string numPlayersStr;
            string numDaysStr;
            int numPlayers;
            int numDays;

            Console.WriteLine("How many players?");
            numPlayersStr = Console.ReadLine();
            Console.WriteLine("How many days?");
            numDaysStr = Console.ReadLine();

            numPlayers = Convert.ToInt32(numPlayersStr);
            numDays = Convert.ToInt32(numDaysStr);

            Process myWait;

            for (int i = 1; i <= numPlayers; i++)
            {
                ProcessStartInfo myProcess = new ProcessStartInfo();
                myProcess.FileName = "C:\\dCC\\Week4\\Projects\\LemonadeStand\\LemonadeStand\\LemonadeStand\\bin\\Debug\\LemonadeStand.exe";
                myProcess.Arguments = i + " " + numDays;
                myProcess.CreateNoWindow = false;
                myWait = Process.Start(myProcess);
                processes.Add("player" + i, myWait);
                dataList.Add(new List<TrackedData>());
                playerScores.Add(0);
            }

            if (numPlayers > 1)
            {

                for (int i = 1; i <= numDays; i++)
                {
                    int playerNumMostSales = -1;
                    int playerNumMostEarned = -1;
                    int playerNumHighestRatio = -1;

                    int mostSales = -1;
                    int mostEarned = -1;
                    float highestRatio = -1;

                    for (int j = 1; j <= numPlayers; j++)
                    {
                        while (!File.Exists("c:\\temp\\player" + j + "day" + i + ".bin"))
                        {
                            Thread.Sleep(500);
                        }
                    }

                    for (int j = 1; j <= numPlayers; j++)
                    {
                        TrackedData data = SerializedData.DeserializeDailyData(j, i);
                        dataList[j - 1].Add(data);
                        Console.WriteLine("\n----------\n");
                        Console.WriteLine("Player " + j + " encountered " + dataList[j - 1][i - 1].customerList.Count + " customers today.");
                        Console.WriteLine("Player " + j + " sold " + dataList[j - 1][i - 1].customersBought + " cups of lemonade today.");
                        Console.WriteLine("Player " + j + " spent $" + dataList[j - 1][i - 1].moneySpent + " today.");
                        Console.WriteLine("Player " + j + " earned $" + dataList[j - 1][i - 1].moneyEarned + " today.");

                        if(dataList[j-1][i-1].moneyEarned > mostEarned)
                        {
                            mostEarned = dataList[j - 1][i - 1].moneyEarned;
                            playerNumMostEarned = (j - 1);
                        }
                        else if (dataList[j - 1][i - 1].moneyEarned == mostEarned)
                        {
                            playerNumMostEarned = -1;
                        }

                        if (dataList[j - 1][i - 1].customersBought > mostSales)
                        {
                            mostSales = dataList[j - 1][i - 1].customersBought;
                            playerNumMostSales = (j - 1);
                        }
                        else if (dataList[j - 1][i - 1].customersBought == mostSales)
                        {
                            playerNumMostSales = -1;
                        }

                        float ratio = ((float)dataList[j - 1][i - 1].customersBought) / ((float)dataList[j - 1][i - 1].customerList.Count);
                        Console.WriteLine("Player " + j + " ratio of csales to customers is " + ratio + ".");
                        Console.WriteLine("\n----------\n");

                        if (ratio > highestRatio)
                        {
                            highestRatio = ratio;
                            playerNumHighestRatio = (j - 1);
                        }
                        else if (ratio == highestRatio)
                        {
                            playerNumHighestRatio = -1;
                        }
                    }

                    if (playerNumMostEarned != -1)
                    {
                        Console.WriteLine("Player " + (playerNumMostEarned + 1) + " earned the most money today and gets a point.");
                        playerScores[playerNumMostEarned]++;
                    }
                    else
                    {
                        Console.WriteLine("The top players earned the same amount of money, so no points are awarded.");
                    }

                    if (playerNumMostSales != -1)
                    {
                        Console.WriteLine("Player " + (playerNumMostSales + 1) + " made the most sales today and gets a point.");
                        playerScores[playerNumMostSales]++;
                    }
                    else
                    {
                        Console.WriteLine("The top players made the same number of sales, so no points are awarded.");
                    }

                    if (playerNumHighestRatio != -1)
                    {
                        Console.WriteLine("Player " + (playerNumHighestRatio + 1) + " had the greatest ratio of customers to sales today and gets a point.");
                        playerScores[playerNumHighestRatio]++;
                    }
                    else
                    {
                        Console.WriteLine("The top players had the same ratio of sales to customers, so no points are awarded.");
                    }

                    for (int j = 1; j <= numPlayers; j++)
                    {
                        Console.WriteLine("Player " + j + " has a score of " + playerScores[j - 1] + ".");
                    }


                    for (int j = 1; j <= numPlayers; j++)
                    {
                        File.Delete("c:\\temp\\player" + j + "day" + i + ".bin");
                    }
                }

                int topPlayerScore = -1;
                int topPlayerNum = -1;

                for (int i = 0; i < numPlayers; i++)
                {
                    int totalCustomers = 0;
                    int totalPurchases = 0;
                    int totalSpent = 0;
                    int totalEarned = 0;
                    for (int j = 0; j < dataList[i].Count; j++)
                    {
                        totalCustomers += dataList[i][j].customerList.Count;
                        totalPurchases += dataList[i][j].customersBought;
                        totalSpent += dataList[i][j].moneySpent;
                        totalEarned += dataList[i][j].moneyEarned;
                    }

                    Console.WriteLine("\n----------\n");
                    Console.WriteLine("Player " + (i + 1) + " encountered a total of " + totalCustomers + " customers.");
                    Console.WriteLine("Player " + (i + 1) + " sold a total of " + totalPurchases + " cups of lemonade.");
                    Console.WriteLine("Player " + (i + 1) + " spent a total of $" + totalSpent + ".");
                    Console.WriteLine("Player " + (i + 1) + " earned a total of $" + totalEarned + ".");
                    Console.WriteLine("\n----------\n");

                    if (playerScores[i] > topPlayerScore)
                    {
                        topPlayerScore = playerScores[i];
                        topPlayerNum = i;
                    }
                    else if (playerScores[i] == topPlayerScore)
                    {
                        topPlayerNum = -1;
                    }
                }

                for (int i = 0; i < numPlayers; i++)
                {
                    Console.WriteLine("Player " + (i + 1) + " has " + playerScores[i] + " points.");
                }
                Console.WriteLine("\n----------\n");
                if (topPlayerNum != -1)
                {
                    Console.WriteLine("Player " + (topPlayerNum + 1) + " wins!");
                }
                else
                {
                    Console.WriteLine("The top players are tied!");
                }
            }
            else
            {
                for (int i = 1; i <= numDays; i++)
                {
                    while (!File.Exists("c:\\temp\\player1day" + i + ".bin"))
                    {
                        Thread.Sleep(500);
                    }
                    File.Delete("c:\\temp\\player1day" + i + ".bin");
                }
            }

            foreach (string script in processes.Keys)
            {
                Process process = processes[script];

                process.WaitForExit();
                process.Close();
            }

            Console.WriteLine("Processes Ended.");
            Console.ReadKey();
        }
    }
}
