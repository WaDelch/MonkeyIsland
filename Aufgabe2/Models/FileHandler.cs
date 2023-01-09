using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MonkeyIsland1.Models
{
    internal static class FileHandler
    {
        static string savePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.bin";
        static BinaryFormatter bf = new BinaryFormatter();

        public static void SaveGame()
        {
            using (FileStream fs = File.Open(savePath, FileMode.Create))
            {
                bf.Serialize(fs, Program.pirates);
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
