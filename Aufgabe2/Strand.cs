using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Strand : Lokation
    {

        public void Event(Pirat ePirat)
        {
            do
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetBezeichnung()} ===~~~");
                Animation.RPGPrint("Du gräbst nach Schätzen.");
                System.Threading.Thread.Sleep(200);
                Animation.DigSite();
                randomZahl = rnd.Next(1, 101);
                if (randomZahl < 16) // 15% Chance 10 Taler zu finden
                {
                    Animation.RPGPrint("Du hast einen Sack mit zehn Talern gefunden!");
                    ePirat.SetTaler(ePirat.GetTaler() + 10);
                }
                else if (randomZahl < 81) // 65% Chance einen Taler zu finden
                {
                    Animation.RPGPrint("Du hast einen Taler gefunden!");
                    ePirat.SetTaler(ePirat.GetTaler() + 1);
                }
                else
                    Animation.RPGPrint("Du hast nichts gefunden.");
                Animation.RPGPrint($"Du hast jetzt {ePirat.GetTaler()} Taler.");
                Animation.RPGPrint("Willst du weitergraben? (j = ja)\n" + Program.menue);
            } while (Console.ReadKey().KeyChar == 'j');
        }
    }
}
