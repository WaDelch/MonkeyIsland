using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MonkeyIsland1.Models
{
    internal static class FileHandler
    {
        static string savePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.bin";
        static BinaryFormatter bf = new BinaryFormatter();

        public static void SaveGame(List<Pirate> p)
        {
            using (FileStream fs = File.Open(savePath, FileMode.Create))
            {
                bf.Serialize(fs, p);
            }
        }

        public static List<Pirate> LoadGame()
        {
            using (FileStream fs = File.Open(savePath, FileMode.Open))
            {
               return (List<Pirate>)bf.Deserialize(fs);
            }
        }
    }
}
