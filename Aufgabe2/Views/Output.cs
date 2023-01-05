using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1.Views
{
    internal class Output
    {
        public void ShowMenue(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i]}");
            }
        }

        public void ShowStats(string[] stats)
        {

        }
    }
}
