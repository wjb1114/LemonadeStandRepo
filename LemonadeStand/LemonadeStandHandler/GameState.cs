using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Diagnostics;
using LemonadeStand;

namespace LemonadeStandHandler
{
    class GameState
    {
        List<List<TrackedData>> dataList;
        List<int> playerScores;
        Dictionary<string, Process> processes;
        string numPlayersStr;
        string numDaysStr;
        readonly string key;
        int numPlayers;
        int numDays;

        public GameState()
        {
            dataList = new List<List<TrackedData>>();
            playerScores = new List<int>();
            processes = new Dictionary<string, Process>();
            key = SeededData.key;
        }

        public void RunGameState()
        {
            SetRunParameters();
            InitializeGames();
            

            if (numPlayers > 1)
            {
                MultiPlayerGame();                
            }
            else
            {
                SinglePlayerGame();
            }

            EndGameCleanup();
        }

        void SetRunParameters()
        {
            bool validNum = true;
            do
            {
                Console.WriteLine("\n----------\n");
                Console.WriteLine("How many players? Maximum of 8 and minimum of 1.");
                numPlayersStr = Console.ReadLine();
                Console.WriteLine("How many days? Minimum of 7.");
                numDaysStr = Console.ReadLine();
                Console.Clear();

                try
                {
                    numPlayers = Convert.ToInt32(numPlayersStr);
                    numDays = Convert.ToInt32(numDaysStr);
                }
                catch (Exception)
                {
                    numPlayers = 0;
                    numDays = 0;
                    Console.WriteLine("Please enter valid positive numbers. You can not have more than 8 players.");
                    validNum = false;
                }
                if ((numPlayers > 8 || numPlayers < 1) && validNum == true)
                {
                    Console.WriteLine("Please enter a numer of players at least 1 and at most 8.");
                }
                if (numDays < 7 && validNum == true)
                {
                    Console.WriteLine("Please enter a number of days that is at least 7.");
                }
                if (validNum == false)
                {
                    validNum = true;
                }
            }
            while (numPlayers > 8 || numPlayers < 1 || numDays < 6);
        }

        void InitializeGames()
        {
            Process myWait;

            for (int i = 1; i <= numPlayers; i++)
            {
                ProcessStartInfo myProcess = new ProcessStartInfo();
                myProcess.FileName = "C:\\dCC\\Week4\\Projects\\LemonadeStand\\LemonadeStand\\LemonadeStand\\bin\\Debug\\LemonadeStand.exe";
                myProcess.Arguments = i + " " + numDays + " " + key;
                myProcess.CreateNoWindow = false;
                myWait = Process.Start(myProcess);
                processes.Add("player" + i, myWait);
                dataList.Add(new List<TrackedData>());
                playerScores.Add(0);
            }
        }

        void EndGameCleanup()
        {
            foreach (string script in processes.Keys)
            {
                Process process = processes[script];

                process.WaitForExit();
                process.Close();
            }

            Console.WriteLine("Processes Ended.");
            Console.ReadKey();
        }

        void SinglePlayerGame()
        {
            for (int i = 1; i <= numDays; i++)
            {
                WaitForDayEnd(1, i);
                File.Delete("c:\\temp\\player1day" + i + ".bin");
            }
        }

        void MultiPlayerGame()
        {
            DisplayPlayerData();
            DisplayWinnerData();
            
        }

        void WaitForDayEnd(int playerNumber, int dayNumber)
        {
            while (!File.Exists("c:\\temp\\player" + playerNumber +"day" + dayNumber + ".bin"))
            {
                Thread.Sleep(500);
            }
        }

        void DisplayPlayerData()
        {
            for (int i = 1; i <= numDays; i++)
            {
                for (int j = 1; j <= numPlayers; j++)
                {
                    WaitForDayEnd(j, i);
                }

                int playerNumMostSales = -1;
                int playerNumMostEarned = -1;
                int playerNumHighestRatio = -1;

                int mostSales = -1;
                int mostEarned = -1;
                float highestRatio = -1;

                for (int j = 1; j <= numPlayers; j++)
                {
                    TrackedData data = SerializedData.DeserializeDailyData(j, i);
                    dataList[j - 1].Add(data);
                    Console.WriteLine("\n----------\n");
                    Console.WriteLine("Player " + j + " encountered " + dataList[j - 1][i - 1].CustomerList.Count + " customers today.");
                    Console.WriteLine("Player " + j + " sold " + dataList[j - 1][i - 1].CustomersBought + " cups of lemonade today.");
                    Console.WriteLine("Player " + j + " spent $" + dataList[j - 1][i - 1].MoneySpent + " today.");
                    Console.WriteLine("Player " + j + " earned $" + dataList[j - 1][i - 1].MoneyEarned + " today.");

                    if (dataList[j - 1][i - 1].MoneyEarned > mostEarned)
                    {
                        mostEarned = dataList[j - 1][i - 1].MoneyEarned;
                        playerNumMostEarned = j - 1;
                    }
                    else if (dataList[j - 1][i - 1].MoneyEarned == mostEarned)
                    {
                        playerNumMostEarned = -1;
                    }

                    if (dataList[j - 1][i - 1].CustomersBought > mostSales)
                    {
                        mostSales = dataList[j - 1][i - 1].CustomersBought;
                        playerNumMostSales = j - 1;
                    }
                    else if (dataList[j - 1][i - 1].CustomersBought == mostSales)
                    {
                        playerNumMostSales = -1;
                    }

                    float ratio = ((float)dataList[j - 1][i - 1].CustomersBought) / ((float)dataList[j - 1][i - 1].CustomerList.Count);
                    Console.WriteLine("Player " + j + " ratio of sales to customers is " + ratio + ".");
                    Console.WriteLine("\n----------\n");

                    if (ratio > highestRatio)
                    {
                        highestRatio = ratio;
                        playerNumHighestRatio = j - 1;
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

                Console.WriteLine("\n----------\n");

                for (int j = 1; j <= numPlayers; j++)
                {
                    Console.WriteLine("Player " + j + " has a score of " + playerScores[j - 1] + ".");
                }


                for (int j = 1; j <= numPlayers; j++)
                {
                    File.Delete("c:\\temp\\player" + j + "day" + i + ".bin");
                }
            }
        }

        void DisplayWinnerData()
        {
            int topPlayerNum = GetEndGameData();

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

        int GetEndGameData()
        {
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
                    totalCustomers += dataList[i][j].CustomerList.Count;
                    totalPurchases += dataList[i][j].CustomersBought;
                    totalSpent += dataList[i][j].MoneySpent;
                    totalEarned += dataList[i][j].MoneyEarned;
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
            return topPlayerNum;
        }
    }
}
