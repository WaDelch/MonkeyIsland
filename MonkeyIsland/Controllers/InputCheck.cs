using System;
using System.Linq;

namespace MonkeyIsland1.Controllers
{
    internal static class InputCheck
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

        public static bool CheckString(string s = "j")
        {
            if (Console.ReadLine().ToLower() == s)
                return true;
            else
                return false;
        }
    }
}
