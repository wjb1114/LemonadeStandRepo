using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LemonadeStand
{
    static class SerializedData
    {
        public static void SerializeDataEnd(List<TrackedData> objList, int playerNum)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "player" + playerNum + ".bin");

            try
            {
                using (Stream stream = File.Open(serializationFile, FileMode.Create))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    bformatter.Serialize(stream, objList);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void SerializeDataDaily(TrackedData data, int playerNum, int numDay)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "player" + playerNum + "day" + numDay + ".bin");

            try
            {
                using (Stream stream = File.Open(serializationFile, FileMode.Create))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    bformatter.Serialize(stream, data);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
