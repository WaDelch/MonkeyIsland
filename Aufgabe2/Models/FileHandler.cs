using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1.Models
{
    internal static class FileHandler
    {
        static string savePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.bin";
        public static void SaveGame(List<Pirat> p)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(savePath, FileMode.Create))
            {
                bf.Serialize(fs, p);
            }
        }

        public static List<Pirat> LoadGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(savePath, FileMode.Open))
            {
               return (List<Pirat>)bf.Deserialize(fs);
            }
        }
    }
}
