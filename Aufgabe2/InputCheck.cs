using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class InputCheck
    {
        public static bool CheckInt(out int uinput)
        {
            uinput = int.TryParse(Console.ReadLine(), out uinput) ? uinput : 0;
            if (uinput == 0)
                return false;
            else
                return true;
        }

        public static bool CheckUInt(out uint uinput)
        {
            uinput = uint.TryParse(Console.ReadLine(), out uinput) ? uinput : 0;
            if (uinput == 0)
                return false;
            else
                return true;
        }

        public static bool CheckAlphaNum(string s)
        {
            if (s.All(char.IsLetterOrDigit) && s != "")
                return true;
            else
                return false;
        }
    }
}
