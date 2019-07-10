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
        public static void SerializeData(List<TrackedData> objList, int playerNum)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "player" + playerNum + ".bin");

            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, objList);
            }
        }
    }
}
