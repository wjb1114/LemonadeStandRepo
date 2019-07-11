using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LemonadeStand;

namespace LemonadeStandHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TrackedData> dataList;
            Dictionary<string, Process> processes = new Dictionary<string, Process>(); ;
            string numPlayersStr;
            string numDaysStr;
            int numPlayers;
            int numDays;

            Console.WriteLine("How many players?");
            numPlayersStr = Console.ReadLine();
            Console.WriteLine("How many days?");
            numDaysStr = Console.ReadLine();

            numPlayers = System.Convert.ToInt32(numPlayersStr);
            numDays = System.Convert.ToInt32(numDaysStr);

            Process myWait;

            for (int i = 1; i <= numPlayers; i++)
            {
                ProcessStartInfo myProcess = new ProcessStartInfo();
                myProcess.FileName = "C:\\dCC\\Week4\\Projects\\LemonadeStand\\LemonadeStand\\LemonadeStand\\bin\\Debug\\LemonadeStand.exe";
                myProcess.Arguments = i + " " + numDays;
                myProcess.CreateNoWindow = false;
                myWait = Process.Start(myProcess);
                processes.Add("player" + i, myWait);
            }

            for (int i = 1; i <= numDays; i++)
            {
                for (int j = 1; j <= numPlayers; j++)
                {
                    while (!System.IO.File.Exists("c:\\temp\\player" + j + "day" + i + ".bin"))
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                }

                for (int j = 1; j <= numPlayers; j++)
                {
                    TrackedData data = DeserializedData.DeserializeDailyData(j, i);
                    Console.WriteLine("\n----------\n");
                    Console.WriteLine("Player " + j + " encountered " + data.customerList.Count + " customers today.");
                    Console.WriteLine("Player " + j + " sold " + data.customersBought + " cups of lemonade today.");
                    Console.WriteLine("Player " + j + " spent $" + data.moneySpent + " today.");
                    Console.WriteLine("Player " + j + " earned $" + data.moneyEarned + " today.");
                    Console.WriteLine("\n----------\n");
                }

                for (int j = 1; j <= numPlayers; j++)
                {
                    System.IO.File.Delete("c:\\temp\\player" + j + "day" + i + ".bin");
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
