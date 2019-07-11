using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LemonadeStand;

namespace LemonadeStandHandler
{
    static class DeserializedData
    {
        public static TrackedData DeserializeDailyData(int playerNum, int numDay)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "player" + playerNum + "day" + numDay + ".bin");
            TrackedData data;

            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                data = (TrackedData)bformatter.Deserialize(stream);
            }

            return data;
        }
    }
}
